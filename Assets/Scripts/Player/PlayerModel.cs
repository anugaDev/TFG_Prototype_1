using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Player
{
    public class PlayerModel
    {
        private PlayerConfig playerConfig;

        public ReactiveProperty<BaseItemView> carriedItem;
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
            
           carriedItem = new ReactiveProperty<BaseItemView>();
        }
    }
}