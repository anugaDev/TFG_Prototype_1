using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using Reliquary.Player;
using Reliquary.Relic;
using UnityEngine;

namespace Reliquary.Hub
{
    public class OnRelicPlaced
    {
        private HubModel hubModel;
        private PlayerModel playerModel;

        public OnRelicPlaced(HubModel _hubModel, PlayerModel _playerModel)
        {
            hubModel = _hubModel;
            playerModel = _playerModel;

        }

        public void Execute()
        {
            playerModel.isPlacing = false;
            playerModel.carriedItem.Value = null;
        }
    }
}