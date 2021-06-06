using Reliquary.Stalker;
using UniRx;

namespace Level
{
    public class LevelModel
    {
        public readonly StalkerModel[] enemies;
        public int enemiesInCombat;
        
        public LevelModel(StalkerModel[] _enemies)
        {
            enemiesInCombat = 0;
            enemies = _enemies;
        }
    }
}
