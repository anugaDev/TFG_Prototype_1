using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class OnPlayerTouchedCommand
    {
        private PlayerModel playerModel;
        
        public OnPlayerTouchedCommand(PlayerModel _playerModel)
        {
            playerModel = _playerModel;
        }

        public void Execute(StalkerModel _stalkerModel)
        {
            _stalkerModel.currentState.Value = EEnemyStates.Wander;

            
            playerModel.isDead.Value = true;

        }
    }
}
