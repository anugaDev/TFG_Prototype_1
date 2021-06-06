using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using Reliquary.Relic;

namespace Reliquary.Hub
{
    public class HubModel
    {
        
        public Vector3 hubPosition;
        public readonly ReactiveProperty<float> playerProximity;
        public readonly ReactiveCollection<RelicView> acquiredRelics;
        private float placingTime = 2.0f;
        public int relicsToWin = 3;

        public HubModel()
        {
            playerProximity = new ReactiveProperty<float>();
            acquiredRelics = new ReactiveCollection<RelicView>();
        }

        public float GetPlacingTime()
        {
            return placingTime;
        }

        public bool AllRelicsReturned()
        {
            return acquiredRelics.Count >= relicsToWin;
        }
    }
}
