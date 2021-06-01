using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class OnPlayerTouched
    {
        private PlayerModel playerModel;
        
        public OnPlayerTouched()
        {
            
        }

        public void Execute(StalkerModel _stalkerModel)
        {
            _stalkerModel.currentState.Value = EStalkerStates.Patrol;

            
            playerModel.isDead.Value = true;

        }
    }
}
