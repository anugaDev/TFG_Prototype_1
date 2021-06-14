using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using Reliquary.Player;
using Reliquary.Relic;
using UnityEngine;

namespace Reliquary.Hub
{
    public class OnPlayerRelicPlacingEnded
    {
        private PlayerModel playerModel;

        public OnPlayerRelicPlacingEnded(PlayerModel _playerModel)
        {
            playerModel = _playerModel;

        }

        public void Execute()
        {
            playerModel.isPlacing = false;
            playerModel.carriedItem.Value = null;
        }
    }
}