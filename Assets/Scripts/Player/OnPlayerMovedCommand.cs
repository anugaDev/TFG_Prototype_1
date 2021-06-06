using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using UnityEngine;

namespace Reliquary.Player
{

    public class OnPlayerMovedCommand
    {
        private HubModel hubModel;
        public OnPlayerMovedCommand(HubModel _hubModel)
        {
            hubModel = _hubModel;
        }

        public void Execute(Vector3 newPosition)
        {
            hubModel.playerProximity.Value = Vector3.Distance(hubModel.hubPosition, newPosition);
        }
    }
}