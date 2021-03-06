﻿using SRTPluginProviderMGU.Enumerations;
using System;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class CharacterEntry : BaseNotifyModel, IEquatable<CharacterEntry>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (IsAlive)
                    return String.Format("{0} {1} / {2} ({3:P1})", Name, CurrentHP, MaximumHP, Percentage);
                else
                    return String.Format("{0} DEAD / DEAD (0%)", Name);
            }
        }

        public string DebugMessage =>
            $"{CurrentHP}:{MaximumHP}:{Convert.ToInt32(IsAlive)}:{Convert.ToInt32(IsPoison)}:{Room.Id}:{Character}";

        public string HealthMessage =>
            $"{Percentage:P0}";

        public int Index { get; private set; }

        public CharacterEnumeration Character { get; private set; }

        public RoomEntry Room { get; } = new RoomEntry();

        public InventoryEntry Equipment { get; } = new InventoryEntry(0);

        internal InventoryEntry[] _inventory = new InventoryEntry[18];
        public InventoryEntry[] Inventory
        {
            get
            {
                if (_inventory[0] == null)
                {
                    for (int i = 0; i < _inventory.Length; i++)
                        _inventory[i] = new InventoryEntry(i);
                    OnPropertyChanged();
                }

                return _inventory;
            }
        }

        public int MaximumHP { get; } = 100;

        internal int _currentHP;
        public int CurrentHP
        {
            get => _currentHP;
            set => SetField(ref _currentHP, value, "CurrentHP", "IsAlive", "IsFine", "IsCaution", "IsDanger", "DisplayHP", "Percentage", "Status", "HealthMessage", "DebugMessage");
        }

        public int DisplayHP
            => Math.Max(CurrentHP, 0);

        public float Percentage
            => IsAlive ? (float)DisplayHP / MaximumHP : 0f;

        public bool IsAlive
            => CurrentHP > 0;

        public bool IsFine
            => CurrentHP >= 50;

        public bool IsCaution
            => CurrentHP >= 30 && CurrentHP < 50;

        public bool IsDanger
            => CurrentHP < 30;

        internal byte _isPoison;
        public bool IsPoison
        {
            get => _isPoison == 1 ? true : false;
            set => SetField(ref _isPoison, (byte)(value ? 1 : 0), "IsPoison", "Status", "DebugMessage");
        }

        public string Status
        {
            get
            {
                if (!IsAlive)
                    return "Dead";
                else if (IsPoison)
                    return "Poison";
                else if (IsDanger)
                    return "Danger";
                else if (IsCaution)
                    return "Caution";
                else
                    return "Fine";
            }
        }

        public string Name
        {
            get
            {
                switch (Character)
                {
                    case CharacterEnumeration.Diane:
                        return "Diane Matlock";

                    case CharacterEnumeration.Martin:
                        return "Martin Karne";

                    default:
                        return "Uji Kenzo";
                }
            }
        }

        public string FirstName
        {
            get
            {
                switch (Character)
                {
                    case CharacterEnumeration.Diane:
                        return "Diane";

                    case CharacterEnumeration.Martin:
                        return "Martin";

                    default:
                        return "Uji";
                }
            }
        }

        public string DisplayName
        {
            get
            {
                switch (Character)
                {
                    case CharacterEnumeration.Diane:
                        return "Matlock";

                    case CharacterEnumeration.Martin:
                        return "Karne";

                    default:
                        return "Kenzo";
                }
            }
        }

        public CharacterEntry(int index)
        {
            Index = index;
            Character = (CharacterEnumeration)index;
        }

        public override bool Equals(object obj) =>
            Equals(obj as CharacterEntry);

        public bool Equals(CharacterEntry other) =>
            other != null &&
            Index == other.Index;

        public override int GetHashCode() =>
            -2134847229 + Index.GetHashCode();
    }
}