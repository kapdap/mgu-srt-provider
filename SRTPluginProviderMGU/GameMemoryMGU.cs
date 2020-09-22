using SRTPluginProviderMGU.Enumerations;
using SRTPluginProviderMGU.Models;

namespace SRTPluginProviderMGU
{
    public class GameMemoryMGU : BaseNotifyModel, IGameMemoryMGU
    {
        public ProcessEntry Process { get; } = new ProcessEntry();
        public GameVersion Version { get; } = new GameVersion();
        public StateEntry State { get; } = new StateEntry();
        public TimeEntry IGT { get; } = new TimeEntry();
        private CharacterEntry[] _characters = new CharacterEntry[3];
        public CharacterEntry[] Characters
        {
            get
            {
                if (_characters[0] == null)
                {
                    for (int i = 0; i < _characters.Length; i++)
                        _characters[i] = new CharacterEntry(i);
                    OnPropertyChanged();
                }

                return _characters;
            }
        }
        private EnemyEntry[] _enemy = new EnemyEntry[24];
        public EnemyEntry[] Enemy
        {
            get
            {
                if (_enemy[0] == null)
                {
                    for (int i = 0; i < _enemy.Length; i++)
                        _enemy[i] = new EnemyEntry(i);
                    OnPropertyChanged();
                }

                return _enemy;
            }
        }
    }
}