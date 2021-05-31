using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Reliquary.Relic;

namespace Reliquary.Hub
{
    public class HubModel
    {
        public enum GameState
        {
            Walking = 0,
            Powering = 1
        }
        public Vector3 hubPosition;
        public readonly ReactiveProperty<float> playerProximity;
        public readonly ReactiveCollection<RelicModel> acquiredRelics;
        private float placingTime = 2.0f;

        public HubModel()
        {
            playerProximity = new ReactiveProperty<float>();
            acquiredRelics = new ReactiveCollection<RelicModel>();
        }

        public float GetPlacingTime()
        {
            return placingTime;
        }
    }
}
