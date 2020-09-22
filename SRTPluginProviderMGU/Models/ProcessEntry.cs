using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class ProcessEntry : BaseNotifyModel, IEquatable<ProcessEntry>
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay => 
            String.Format("{0} ({1})", ProcessName, Id);

        internal int _id;
        public int Id
        {
            get => _id;
            set => SetField(ref _id, value);
        }

        internal string _processName;
        public string ProcessName
        {
            get => _processName;
            set => SetField(ref _processName, value);
        }

        internal int _windowHandle;
        public int WindowHandle
        {
            get => _windowHandle;
            set => SetField(ref _windowHandle, value);
        }

        public override bool Equals(object obj) =>
            Equals(obj as ProcessEntry);

        public bool Equals(ProcessEntry other) =>
            other != null &&
            Id == other.Id &&
            ProcessName == other.ProcessName;

        public override int GetHashCode()
        {
            int hashCode = -1107992127;
            hashCode = hashCode * -1521134295 + Id.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(ProcessName);
            return hashCode;
        }
    }
}