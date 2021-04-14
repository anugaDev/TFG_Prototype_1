using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Item
{
    public class OnItemPickedUp
    {
        private PlayerModel playerModel;

        public OnItemPickedUp(PlayerModel _playerModel)
        {
            playerModel = _playerModel;
        }

        public void Execute(GameObject item)
        {
            var itemView = item.GetComponent<BaseItemView>();

            itemView.PickUp();
            playerModel.carriedItem.Value = itemView;

        }
    }
}