using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using Reliquary.Player;
using Reliquary.Relic;
using UnityEngine;

namespace Reliquary.Hub
{
    public class OnRelicTouchingAltar
    {
        private HubModel hubModel;
        private PlayerModel playerModel;

        public OnRelicTouchingAltar(HubModel _hubModel, PlayerModel _playerModel)
        {
            hubModel = _hubModel;
            playerModel = _playerModel;

        }

        public void Execute(Transform relicTransform, Transform altarTransform, RelicModel relicModel)
        {
            relicTransform.parent = altarTransform.parent;
            relicTransform.position = altarTransform.position;
            relicTransform.localRotation = altarTransform.localRotation;
            
            hubModel.acquiredRelics.Add(relicModel);

            relicModel.isTaken.Value = false;
            relicModel.isPlaced.Value = true;
            playerModel.isPlacing = true;
        }
    }
}
