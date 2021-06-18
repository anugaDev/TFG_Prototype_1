using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class OnPlayerTouchedCommand
    {
        private PlayerModel playerModel;
        private PlayerView playerView;

        
        public OnPlayerTouchedCommand(PlayerModel _playerModel, PlayerView _playerView)
        {
            playerModel = _playerModel;
            playerView = _playerView;
        }

        public void Execute(StalkerModel _stalkerModel)
        {
            _stalkerModel.currentState.Value = EEnemyStates.Wander;

            
            playerModel.isDead.Value = true;
            
            if(playerModel.IsCarryingItem())
                playerView.Controller.Dropitem();
            

        }
    }
}
