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

        [SerializeField] private StalkerInstaller[] stalkers;
        public void Install(OnPlayerTouchedCommand onPlayerTouchedCommand, PlayerModel _playerModel)
        {
            var _enemyModels = new StalkerModel[stalkers.Length];
            
            for (var i = 0; i < stalkers.Length; i++)
            {
                var _enemyModel = new StalkerModel(configuration, enemyConfiguration, _playerModel);
                
                stalkers[i].Install(onPlayerTouchedCommand, _playerModel, _enemyModel);

                _enemyModels[i] = _enemyModel;
            }

            var _model = new LevelModel(_enemyModels);

            new LevelController(_model, view);
        }
    }
}

