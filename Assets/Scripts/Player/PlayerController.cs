using System;
using System.Collections;
using System.Collections.Generic;
using Reliquary.Item;
using UniRx;
using UnityEngine;

namespace Reliquary.Player
{

    public class PlayerController
    {
        private PlayerView view;
        private PlayerModel model;
        private OnItemPickedUpCommand onItemPickedUpCommand;
        private OnItemDroppedCommand _onItemDroppedCommand;
        private OnPlayerMovedCommand onPlayerMovedCommand;

        public PlayerController(PlayerView _view, PlayerModel _model, OnPlayerMovedCommand _onPlayerMovedCommand,
            OnItemPickedUpCommand onItemPickedUpCommand, OnItemDroppedCommand onItemDroppedCommand)
        {
            view = _view;
            model = _model;
            onPlayerMovedCommand = _onPlayerMovedCommand;
            this.onItemPickedUpCommand = onItemPickedUpCommand;
            _onItemDroppedCommand = onItemDroppedCommand;

            view.Controller = this;
            model.SetAvatar(view.gameObject);
            PlayerMoved(view.transform.position);
        }

        public void PickUpItem(GameObject item)
        {
            if (item != null)
            {
                view.CarryItem(item.transform);
                onItemPickedUpCommand.Execute(item);
                view.PlayPickUpSound();
            }
        }

        public bool IsCarryingItem()
        {
            return model.carriedItem.Value != null;
        }

        public bool IsPowering()
        {
            return model.isPlacing;
        }

        public void SetSneaking(bool _sneaking)
        {
            model.isSneaking = _sneaking;
        }
        
        public void Dropitem()
        {
            _onItemDroppedCommand.Execute(model.carriedItem.Value);
            view.PlayDropSound();
        }

        public float GetCurrentSpeed(Vector3 _movementVector)
        {
            if (_movementVector == Vector3.zero || IsPowering() || model.isDead.Value)
            {
                model.currentSpeed = 0; 
                view.StopSteps();

            }
            else
            {
                if (model.isSneaking)
                {
                    view.StopSteps();

                    if (IsCarryingItem())
                    {
                        model.currentSpeed = 0;
                    }
                    else
                    {
                        model.currentSpeed = model.SneakSpeed;
                    }
                }
                else
                {
                    view.PlaySteps();
                    if (IsCarryingItem())
                    {
                        model.currentSpeed = model.CarryingSpeed;
                    }
                    else
                    {
                        model.currentSpeed = model.DefaultSpeed;
                    }
                }
                
            }
            
            return model.currentSpeed;
        }

        public void PlayerMoved(Vector3 currentPosition)
        {
            onPlayerMovedCommand.Execute(currentPosition);
        }

        public bool IsDead()
        {
            return model.isDead.Value;
        }

        public void SetAlive()
        {
            view.transform.position = model.GetSpawmPosition();
            PlayerMoved(view.transform.position);
            model.isDead.Value = false;
        }

        
        public float GetCurrentRotationSpeed()
        {
            return model.RotationSpeed;
        }
    }
}