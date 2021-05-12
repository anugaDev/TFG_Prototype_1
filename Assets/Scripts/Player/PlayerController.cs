using System;
using System.Collections;
using System.Collections.Generic;
using Reliquary.Item;
using UnityEngine;

namespace Reliquary.Player
{

    public class PlayerController
    {
        private PlayerView view;
        private PlayerModel model;
        private OnItemPickedUpCommand onItemPickedUpCommand;
        private OnItemDropped onItemDropped;
        private OnPlayerMovedCommand onPlayerMovedCommand;

        public PlayerController(PlayerView _view, PlayerModel _model, OnPlayerMovedCommand _onPlayerMovedCommand, OnItemPickedUpCommand onItemPickedUpCommand, OnItemDropped _onItemDropped)
        {
            view = _view;
            model = _model;
            onPlayerMovedCommand = _onPlayerMovedCommand;
            this.onItemPickedUpCommand = onItemPickedUpCommand;
            onItemDropped = _onItemDropped;

            view.Controller = this;
        }

        public void PickUpItem(GameObject item)
        {
            item.transform.SetParent(view.transform);
            onItemPickedUpCommand.Execute(item);
        }

        public bool CarryingItem()
        {
            return model.carriedItem.Value != null;
        }

        public void Dropitem()
        {
            onItemDropped.Execute(model.carriedItem.Value);
        }

        public float GetCurrentSpeed()
        {
            return model.DefaultSpeed;
        }

        public void PlayerMoved(Vector3 currentPosition)
        {
            onPlayerMovedCommand.Execute(currentPosition);
        }

        public float GetCurrentRotationSpeed()
        {
            return model.RotationSpeed;
        }
    }
}