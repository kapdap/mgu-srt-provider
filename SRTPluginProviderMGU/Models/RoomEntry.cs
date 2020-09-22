using System;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class RoomEntry : BaseNotifyModel, IEquatable<RoomEntry>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay =>
            String.Format("{0}", Id);

        internal byte _id;
        public byte Id
        {
            get => _id;
            set => SetField(ref _id, value);
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

		internal string GetName()
        {
            switch (Id)
            {
				default:
					return "Unknown";
			}
        }

        public override bool Equals(object obj) =>
            Equals(obj as RoomEntry);

        public bool Equals(RoomEntry other) =>
            other != null &&
            Id == other.Id;

        public override int GetHashCode() =>
            -659867440 + Id.GetHashCode();
    }
}