using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Hub
{
    
    public class HubModel
    {
        public Vector3 hubPosition;
        public ReactiveProperty<float> playerDistance;

        public HubModel()
        {
            playerDistance = new ReactiveProperty<float>();
        }
    }
}
