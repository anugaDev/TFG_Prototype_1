using System.Collections;
using System.Collections.Generic;
using Level;
using Reliquary.Player;
using Reliquary.Stalker;
using UnityEngine;

namespace Reliquary.Level
{
    public class LevelInstaller : MonoBehaviour
    {
        [SerializeField] private LevelConfiguration configuration;
        [SerializeField] private StalkerConfiguration enemyConfiguration;
        [SerializeField] private LevelView view;
        [SerializeField] private StalkerInstaller[] enemyInstallers;
        public void Install(OnPlayerTouchedCommand onPlayerTouchedCommand, PlayerModel _playerModel)
        {
            var _enemyModels = new StalkerModel[enemyInstallers.Length];
            
            for (var i = 0; i < enemyInstallers.Length; i++)
            {
                var _enemyModel = new StalkerModel(configuration, enemyConfiguration, _playerModel);
                
                enemyInstallers[i].Install(onPlayerTouchedCommand, _enemyModel);

                _enemyModels[i] = _enemyModel;
            }

            var _model = new LevelModel(_enemyModels);

            new LevelController(_model, view);
        }
    }
}

