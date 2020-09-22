using ProcessMemory;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SRTPluginProviderMGU.Models
{
    public class BaseNotifyModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string name = null, params string[] properties)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                    return false;

            field = value;

            OnPropertyChanged(name);
            foreach (string property in properties)
                OnPropertyChanged(property);

            return true;
        }

        public unsafe bool SetField<T>(ProcessMemoryHandler _processMemory, IntPtr pointer, ref T field, string name = null, params string[] properties)
            where T : unmanaged
        {
            T value = field;

            fixed (T* p = &field)
            {
                if (typeof(T) == typeof(byte))
                    _processMemory.TryGetByteAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(sbyte))
                    _processMemory.TryGetSByteAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(short))
                    _processMemory.TryGetShortAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(ushort))
                    _processMemory.TryGetUShortAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(int))
                    _processMemory.TryGetIntAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(uint))
                    _processMemory.TryGetUIntAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(long))
                    _processMemory.TryGetLongAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(ulong))
                    _processMemory.TryGetULongAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(float))
                    _processMemory.TryGetFloatAt(pointer, (IntPtr)p);
                else if (typeof(T) == typeof(double))
                    _processMemory.TryGetDoubleAt(pointer, (IntPtr)p);
            }

            if (!EqualityComparer<T>.Default.Equals(field, value))
            {
                OnPropertyChanged(name);
                foreach (string property in properties)
                    OnPropertyChanged(property);
            }

            return true;
        }

        public void SendUpdateEvent(string name) =>
            OnPropertyChanged(name);

        public void SendUpdateEntryEvent() =>
            OnPropertyChanged("UpdateEntry");
    }
}