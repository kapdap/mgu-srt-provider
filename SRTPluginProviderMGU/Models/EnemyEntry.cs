using SRTPluginProviderMGU.Enumerations;
using System;
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
                if (IsEmpty)
                    return String.Format("[#{0}] EMPTY / EMPTY (0%)", Index);
                else if (IsAlive)
                    return String.Format("[#{0}] {1} / {2} ({3:P1}) {4}", Index, CurrentHP, MaximumHP, Percentage, Name);
                else
                    return String.Format("[#{0}] DEAD / DEAD (0%) {1}", Index, Name);
            }
        }

        public string DebugMessage =>
            $"{Index}:{CurrentHP}:{MaximumHP}:{Convert.ToInt32(IsAlive)}:{Convert.ToInt32(IsEmpty)}:{Room.Id}:{Type}";

        public string HealthMessage =>
            $"{DisplayHP} / {MaximumHP} ({Percentage:P0})";

        public int Index { get; private set; }

        public RoomEntry Room { get; } = new RoomEntry();

        internal byte[] _typeMem = new byte[8];
        internal long _type;
        public EnemyEnumeration Type
        {
            get => Enum.IsDefined(typeof(EnemyEnumeration), (EnemyEnumeration)_type) ? (EnemyEnumeration)_type : EnemyEnumeration.None;
            set => SetField(ref _type, (long)value, "Type", "Name", "Percentage", "HealthMessage", "DebugMessage");
        }

        public string Name
        {
            get
            {
                switch (Type)
                {
                    case EnemyEnumeration.Undead:
                        return "Undead";
                    case EnemyEnumeration.Extrude:
                        return "Extrude";
                    default:
                        return "None";
                }
            }
        }

        internal short _currentHP;
        public short CurrentHP
        {
            get => _currentHP;
            set => SetField(ref _currentHP, value, "IsAlive", "CurrentHP", "DisplayHP", "Percentage", "HealthMessage", "DebugMessage");
        }

        public short DisplayHP
            => Math.Max(CurrentHP, (short)0);

        internal short _maximumHP;
        public short MaximumHP
        {
            get => _maximumHP;
            set => SetField(ref _maximumHP, value, "IsAlive", "MaximumHP", "Percentage", "HealthMessage", "DebugMessage");
        }

        public float Percentage
            => IsAlive ? (float)DisplayHP / MaximumHP : 0f;

        public bool IsAlive =>
            CurrentHP > 0 && MaximumHP >= CurrentHP;

        public bool IsEmpty =>
            Type == EnemyEnumeration.None;

        public EnemyEntry(int index) =>
            Index = index;

        public override bool Equals(object obj) => 
            Equals(obj as EnemyEntry);

        public bool Equals(EnemyEntry other) =>
            other != null &&
            Index == other.Index;

        public override int GetHashCode() =>
            -1521134295 + Index.GetHashCode();
    }
}