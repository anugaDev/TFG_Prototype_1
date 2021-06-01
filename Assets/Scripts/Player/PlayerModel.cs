using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Reliquary.Player
{
    public class PlayerModel
    {
        private PlayerConfig playerConfig;

        public readonly ReactiveProperty<BaseItemView> carriedItem;
        public readonly ReactiveProperty<bool> isDead;
        public bool isPlacing;
        public float DefaultSpeed
        {
            get => playerConfig.defaultSpeed;
        }

        public float RotationSpeed
        {
            get => playerConfig.rotationSpeed;
        }

        public PlayerModel(PlayerConfig _playerConfig)
        {
            playerConfig = _playerConfig;
            isPlacing = false;
            isDead= new ReactiveProperty<bool>(false);
            carriedItem = new ReactiveProperty<BaseItemView>(null);
        }
    }
}