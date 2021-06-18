using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Reliquary.Player
{
    public class PlayerModel
    {
        private PlayerConfiguration _playerConfiguration;

        public readonly ReactiveProperty<BaseItemView> carriedItem;
        public readonly ReactiveProperty<bool> isDead;
        public float currentSpeed;
        private GameObject avatar;
        public bool isPlacing;
        public bool isSneaking;
        
        public float DefaultSpeed
        {
            get => _playerConfiguration.defaultSpeed;
        }
        public float CarryingSpeed
        {
            get => _playerConfiguration.carryingSpeed;
        }
        public float SneakSpeed
        {
            get => _playerConfiguration.sneakSpeed;
        }

        public float RotationSpeed
        {
            get => _playerConfiguration.rotationSpeed;
        }

        public Vector3 GetSpawmPosition()
        {
            return _playerConfiguration.spawnPosition;
        }

        public PlayerModel(PlayerConfiguration playerConfiguration)
        {
            _playerConfiguration = playerConfiguration;
            isPlacing = false;
            isSneaking = false;
            isDead = new ReactiveProperty<bool>(false);
            carriedItem = new ReactiveProperty<BaseItemView>(null);
        }

        public GameObject GetAvatar()
        {
            return avatar;
        }

        public void SetAvatar(GameObject _avatar)
        {
            avatar = _avatar;
        }

        public bool IsCarryingItem()
        {
            return carriedItem.Value != null;
        }
    }
}