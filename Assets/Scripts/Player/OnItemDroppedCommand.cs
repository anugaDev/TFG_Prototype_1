using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Item
{
    public class OnItemDroppedCommand
    {
        private PlayerModel playerModel;

        public OnItemDroppedCommand(PlayerModel _playerModel)
        {
            playerModel = _playerModel;
        }

        public void Execute(BaseItemView item)
        {

            item.Drop();
            item.transform.parent = null;
            playerModel.carriedItem.Value = null;

        }
    }
}