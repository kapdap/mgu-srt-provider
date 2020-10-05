using ProcessMemory;
using SRTPluginProviderMGU.Enumerations;
using SRTPluginProviderMGU.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRTPluginProviderMGU
{
    public class GameMemoryMGUScanner : IDisposable
    {
        private Process _process;
        private ProcessMemoryHandler _processMemory;

        internal GamePointers Pointers { get; } = new GamePointers();
        public GameMemoryMGU Memory { get; } = new GameMemoryMGU();

        public bool ProcessRunning => _processMemory != null && _processMemory.ProcessRunning;
        public int ProcessExitCode => _processMemory != null ? _processMemory.ProcessExitCode : 0;

        public bool HasScanned { get; private set; }

        public GameMemoryMGUScanner(Process process) =>
            Initialize(process);

        public void Initialize(Process process)
        {
            if ((_process = process) == null)
                return;

            try
            {
                if (!SetPointerAddresses(GameHashes.DetectVersion(_process?.MainModule?.FileName)))
                    return;
            }
            catch { return; }

            int pid = GetProcessId(_process).Value;
            _processMemory = new ProcessMemoryHandler(pid);

            if (ProcessRunning)
            {
                Pointers.BaseAddress = NativeWrappers.GetProcessBaseAddress(_process.Id, PInvoke.ListModules.LIST_MODULES_32BIT);

                Pointers.WindowHandleId = IntPtr.Add(Pointers.BaseAddress, Pointers._addressWindowHandle);
                Pointers.FrameCount = IntPtr.Add(Pointers.BaseAddress, Pointers._addressFrameCount);
                Pointers.CurrentCharacterId = IntPtr.Add(Pointers.BaseAddress, Pointers._addressCurrentCharacterId);
                Pointers.CurrentRoomId = IntPtr.Add(Pointers.BaseAddress, Pointers._addressCurrentRoomId);
                Pointers.ZombieMaxHP = IntPtr.Add(Pointers.BaseAddress, Pointers._addressZombieMaxHP);

                Pointers.Characters = new IntPtr[Memory.Characters.Length];
                for (int i = 0; i < Pointers.Characters.Length; ++i)
                    Pointers.Characters[i] = IntPtr.Add(Pointers.BaseAddress, Pointers._addressCharacters + (0xAC4 * i));

                Pointers.Inventory = new IntPtr[Memory.Characters.Length, 18];
                for (int i = 0; i < Pointers.Inventory.GetLength(0); ++i)
                    for (int j = 0; j < Pointers.Inventory.GetLength(1); ++j)
                        Pointers.Inventory[i, j] = IntPtr.Add(Pointers.BaseAddress, Pointers._addressInventory + (0x438 * i) + (0x3c * j));

                Pointers.Enemy = new IntPtr[Memory.Enemy.Length];
                for (int i = 0; i < Pointers.Enemy.Length; ++i)
                    Pointers.Enemy[i] = IntPtr.Add(Pointers.BaseAddress, Pointers._addressEnemy + (0xAC4 * i));

                Update();
            }
        }

        public bool SetPointerAddresses(GameVersion version)
        {
            switch (version)
            {
                case GameVersion.MGU_1_1:
                case GameVersion.MGU_1_2:
                case GameVersion.MGU_1_3:
                    Pointers._addressWindowHandle = 0x1B57D8;
                    Pointers._addressFrameCount = 0x1BCF64;
                    Pointers._addressCurrentCharacterId = 0x1BCF60;
                    Pointers._addressCurrentRoomId = 0x1BB519;
                    Pointers._addressZombieMaxHP = 0x1BCF72;
                    Pointers._addressCharacters = 0x1BD0A8;
                    Pointers._addressInventory = 0x35196C;
                    Pointers._addressEnemy = 0x1BF088;
                    return true;
            }

            return false;
        }

        public unsafe void Update()
        {
            Memory.Process.Id = GetProcessId(_process).Value;
            Memory.Process.ProcessName = GetProcessName(_process);
            if (Memory.Process.SetField(_processMemory, Pointers.WindowHandleId, ref Memory.Process._windowHandleId, "WindowHandleId"))
                Memory.Process.WindowHandle = new IntPtr(Memory.Process.WindowHandleId);
        }

        public unsafe IGameMemoryMGU Refresh()
        {
            Memory.IGT.SetField(_processMemory, Pointers.FrameCount, ref Memory.IGT._frameCount, "FrameCount", "Calculated", "TimeStamp", "FormattedString");

            Memory.State.CurrentRoom.SetField(_processMemory, Pointers.CurrentRoomId, ref Memory.State.CurrentRoom._id, "Id");
            Memory.State.SetField(_processMemory, Pointers.CurrentCharacterId, ref Memory.State._currentCharacterId, "CurrentCharacterId");
            Memory.State.CurrentCharacter = Memory.Characters[(int)Memory.State.CurrentCharacterId];

            RefreshCharacters();
            RefreshEnemy();

            HasScanned = true;

            return Memory;
        }

        public unsafe void RefreshCharacters()
        {
            for (int i = 0; i < Memory.Characters.Length; ++i)
            {
                CharacterEntry entry = Memory.Characters[i];
                IntPtr pointer = Pointers.Characters[i];

                entry.SetField(_processMemory, IntPtr.Add(pointer, 0x0), ref entry._currentHP, "CurrentHP", "IsAlive", "IsFine", "IsCaution", "IsDanger", "DisplayHP", "Percentage", "StatusName", "HealthMessage", "DebugMessage");
                entry.SetField(_processMemory, IntPtr.Add(pointer, 0x4), ref entry._isPoison, "IsPoison", "StatusName", "DebugMessage");
                entry.Room.SetField(_processMemory, IntPtr.Add(pointer, 0xA), ref entry.Room._id, "Id");

                RefreshInventory(entry);
            }
        }

        public unsafe void RefreshInventory(CharacterEntry character)
        {
            for (int i = 0; i < character.Inventory.Length; ++i)
            {
                InventoryEntry entry = character.Inventory[i];
                IntPtr pointer = Pointers.Inventory[character.Index, i];

                entry.SetField(_processMemory, IntPtr.Add(pointer, 0x00), ref entry._type, "Type");
                entry.SetField(_processMemory, IntPtr.Add(pointer, 0x10), ref entry._quantity, "Quantity");

                if (entry.Type != ItemEnumeration.None)
                    entry.IsEmpty = false;
                else
                    entry.Clear();
            }
        }

        public unsafe void RefreshEnemy()
        {
            for (int i = 0; i < Memory.Enemy.Length; ++i)
            {
                EnemyEntry entry = Memory.Enemy[i];
                IntPtr pointer = Pointers.Enemy[i];

                fixed (byte* p = entry._typeMem)
                {
                    byte[] value = new byte[entry._typeMem.Length];
                    entry._typeMem.CopyTo(value, 0);

                    _processMemory.TryGetByteArrayAt(IntPtr.Add(pointer, 0x90A), 7, (IntPtr)p);

                    if (!value.Equals(entry._typeMem))
                    {
                        entry._type = BitConverter.ToInt64(entry._typeMem);
                        entry.SendUpdateEvent("IsEmpty", "Type", "Name", "DebugMessage");
                    }
                }

                if (entry.Type == EnemyEnumeration.Undead)
                    entry.SetField(_processMemory, Pointers.ZombieMaxHP, ref entry._maximumHP, "IsAlive", "MaximumHP", "Percentage", "HealthMessage", "DebugMessage");
                else
                    entry.MaximumHP = 100;
                
                entry.SetField(_processMemory, IntPtr.Add(pointer, 0x6C), ref entry._currentHP, "IsAlive", "CurrentHP", "DisplayHP", "Percentage", "HealthMessage", "DebugMessage");

                if (entry.Room.SetField(_processMemory, IntPtr.Add(pointer, 0x76), ref entry.Room._id, "Id"))
                    entry.SendUpdateEvent("DebugMessage");
            }
        }

        private int? GetProcessId(Process process) => process?.Id;

        private string GetProcessName(Process process) => process?.ProcessName;

        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _process?.Dispose();
                    _process = null;
                    _processMemory?.Dispose();
                    _processMemory = null;
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}