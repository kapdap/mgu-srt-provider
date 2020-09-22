using SRTPluginProviderMGU.Enumerations;
using System;
using System.Diagnostics;

namespace SRTPluginProviderMGU.Models
{
    [DebuggerDisplay("{_DebuggerDisplay,nq}")]
    public class StateEntry : BaseNotifyModel
    {
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        public string _DebuggerDisplay => 
            String.Format("{0} {1}", CurrentCharacter?.Name, CurrentRoom.Id);

        internal byte _currentCharacterId;
        public CharacterEnumeration CurrentCharacterId
        {
            get => (CharacterEnumeration)_currentCharacterId;
            set => SetField(ref _currentCharacterId, (byte)value);
        }

        internal CharacterEntry _currentCharacter;
        public CharacterEntry CurrentCharacter
        {
            get => _currentCharacter;
            set => SetField(ref _currentCharacter, value);
        }

        public RoomEntry CurrentRoom { get; } = new RoomEntry();
    }
}
