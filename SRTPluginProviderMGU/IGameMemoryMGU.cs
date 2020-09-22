using SRTPluginProviderMGU.Models;
using SRTPluginProviderMGU.Enumerations;

namespace SRTPluginProviderMGU
{
    public interface IGameMemoryMGU
    {
        public ProcessEntry Process { get; }
        public StateEntry State { get; }
        public TimeEntry IGT { get; }
        public CharacterEntry[] Characters { get; }
        public EnemyEntry[] Enemy { get; }
    }
}