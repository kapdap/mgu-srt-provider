﻿using System;

namespace SRTPluginProviderMGU
{
    internal class GamePointers
    {
        internal GameVersion Version { get; } = new GameVersion();

        internal int _addressWindowHandle;
        internal int _addressFrameCount;
        internal int _addressCurrentRoomId;
        internal int _addressCurrentCharacterId;
        internal int _addressZombieMaxHP;
        internal int _addressCharacters;
        internal int _addressInventory;
        internal int _addressEnemy;

        internal IntPtr BaseAddress { get; set; }
        internal IntPtr WindowHandleId { get; set; }
        internal IntPtr FrameCount { get; set; }
        internal IntPtr CurrentRoomId { get; set; }
        internal IntPtr CurrentCharacterId { get; set; }
        internal IntPtr ZombieMaxHP { get; set; }
        internal IntPtr[] Characters { get; set; }
        internal IntPtr[,] Inventory { get; set; }
        internal IntPtr[] Enemy { get; set; }
    }
}