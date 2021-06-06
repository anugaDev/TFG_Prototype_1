using System.Collections;
using System.Collections.Generic;
using Level;
using UniRx;
using UnityEngine;

namespace Reliquary.Level
{
    public class LevelController
    {
        private LevelModel model;
        private LevelView view;
        
        public LevelController(LevelModel _model, LevelView _view)
        {
            model = _model;
            view = _view;

            foreach (var enemy in model.enemies)
            {
                enemy.currentState.AsObservable().Subscribe(value =>
                {
                    if (value == EEnemyStates.Chase)
                    {
                        SetCombat();
                    }
                    else if (value != EEnemyStates.Search)
                    {
                        EndCombat();
                    }
                });
            }
        }

        private void SetCombat()
        {
            model.enemiesInCombat++;
            view.SetNewStateParameter(EGameStates.Combat);
        }

        private void EndCombat()
        {
            model.enemiesInCombat--;
            
            if(model.enemiesInCombat <= 0)
                view.SetNewStateParameter(EGameStates.Walking);

        }
       
    }
}
