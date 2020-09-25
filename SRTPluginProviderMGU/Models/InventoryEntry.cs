using SRTPluginProviderMGU.Enumerations;
using System;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class InventoryEntry : BaseNotifyModel, IEquatable<InventoryEntry>
    {
        /// <summary>
        /// Debugger display message.
        /// </summary>
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay
        {
            get
            {
                if (!IsEmpty)
                    return string.Format("[#{0}] Slot {1} Item {2} Quantity {3}", Index, Slot, Type, Quantity);
                else
                    return string.Format("[#{0}] Empty Slot", Index);
            }
        }

        public int Index { get; private set; }

        internal byte _type = 0;
        public ItemEnumeration Type
        {
            get => (ItemEnumeration)_type;
            set => SetField(ref _type, (byte)value);
        }

        internal string _name;
        public string Name
        {
            get
            {
                if (String.IsNullOrEmpty(_name))
                    SetField(ref _name, GetName());
                return _name;
            }
            set => SetField(ref _name, value);
        }

        internal short _quantity;
        public short Quantity
        {
            get => _quantity;
            set => SetField(ref _quantity, value);
        }

        public bool HasQuantity
        {
            get
            {
                switch (Type)
                {
                    default:
                        return false;
                }
            }
        }

        internal bool _isEquipped;
        public bool IsEquipped
        {
            get => _isEquipped;
            set => SetField(ref _isEquipped, value);
        }

        internal bool _isEmpty = true;
        public bool IsEmpty
        {
            get => _isEmpty;
            set => SetField(ref _isEmpty, value);
        }

        public int Slot { get; set; }
        public int SlotRow { get; set; }
        public int SlotColumn { get; set; }

        public InventoryEntry(int index)
        {
            Index = index;
            Slot = index;
            SlotRow = Slot / 6;
            SlotColumn = Slot % 6;
        }

        internal string GetName()
        {
            switch (Type)
            {
                case ItemEnumeration.None:
                default:
                    return "None";
            }
        }

        public void Clear()
        {
            if (IsEmpty) return;

            IsEmpty = true;

            _type = 0;
            _name = GetName();
            _quantity = 0;
            _isEquipped = true;

            OnPropertyChanged("Type");
            OnPropertyChanged("Name");
            OnPropertyChanged("Quantity");
            OnPropertyChanged("HasQuantity");
            OnPropertyChanged("IsEquipped");
            OnPropertyChanged("IsEmpty");
        }

        public override bool Equals(object obj) =>
            Equals(obj as InventoryEntry);

        public bool Equals(InventoryEntry other) =>
            other != null &&
            Index == other.Index;

        public override int GetHashCode() =>
            -2005450498 + Index.GetHashCode();
    }
}