using SRTPluginProviderMGU.Enumerations;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class EnemyEntry : BaseNotifyModel, IEquatable<EnemyEntry>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (IsAlive)
                    return String.Format("[#{0}] {1} / {2} ({3:P1}) {4}", Index, CurrentHP, MaximumHP, Percentage, Name);
                if (IsEmpty)
                    return String.Format("[#{0}] EMPTY / EMPTY (0%)", Index);
                else
                    return String.Format("[#{0}] DEAD / DEAD (0%) {1}", Index, Name);
            }
        }

        public string DebugMessage =>
            $"{CurrentHP}:{MaximumHP}:{Convert.ToInt32(IsEmpty)}:{Convert.ToInt32(IsAlive)}:{Convert.ToInt32(Type)}";

        public string HealthMessage =>
            $"{DisplayHP} {Percentage:P0}";

        public int Index { get; private set; }

        internal byte _type = 0;
        public EnemyEnumeration Type
        {
            get => (EnemyEnumeration)_type;
            set => SetField(ref _type, (byte)value, "Type", "Name", "Percentage", "HealthMessage", "DebugMessage");
        }

        public string Name
        {
            get
            {
                switch (Type)
                {
                    case EnemyEnumeration.None:
                    default:
                        return "None";
                }
            }
        }

        internal short _currentHP;
        public short CurrentHP
        {
            get => _currentHP;
            set => SetField(ref _currentHP, value, "CurrentHP", "DisplayHP", "Percentage", "HealthMessage", "DebugMessage");
        }

        public short DisplayHP
            => Math.Max(CurrentHP, (short)0);

        internal short _maximumHP;
        public short MaximumHP
        {
            get => _maximumHP;
            set => SetField(ref _maximumHP, value, "MaximumHP", "Percentage", "HealthMessage", "DebugMessage");
        }

        public float Percentage
            => IsAlive && CurrentHP > 0 && MaximumHP > 0 && MaximumHP >= CurrentHP ? (float)DisplayHP / MaximumHP : 0f;

        internal byte _isAlive;
        public bool IsAlive
        {
            get => _isAlive == 255;
            set => SetField(ref _maximumHP, (byte)(value ? 255 : 0), "IsAlive", "Percentage", "HealthMessage", "DebugMessage");
        }

        internal bool _isEmpty = true;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetField(ref _isEmpty, value);
        }

        public EnemyEntry(int index) =>
            Index = index;

        public void Clear()
        {
            if (IsEmpty) return;

            IsEmpty = true;

            _type = 0;
            _currentHP = 0;
            _maximumHP = 0;
            _isAlive = 0;

            OnPropertyChanged("Type");
            OnPropertyChanged("Name");
            OnPropertyChanged("CurrentHP");
            OnPropertyChanged("DisplayHP");
            OnPropertyChanged("MaximumHP");
            OnPropertyChanged("Percentage");
            OnPropertyChanged("IsAlive");
            OnPropertyChanged("DebugMessage");
            OnPropertyChanged("HealthMessage");
        }

        public override bool Equals(object obj) => 
            Equals(obj as EnemyEntry);

        public bool Equals(EnemyEntry other) =>
            other != null &&
            Index == other.Index;

        public override int GetHashCode() =>
            -1521134295 + Index.GetHashCode();
    }
}