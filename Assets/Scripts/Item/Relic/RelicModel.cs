using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicModel
    {
        public readonly ReactiveProperty<bool> isTaken;
        public readonly ReactiveProperty<bool> isPlaced;
        public readonly ReactiveProperty<bool> Visible;

        
        public RelicModel()
        {
            isTaken = new ReactiveProperty<bool>();
            isPlaced = new ReactiveProperty<bool>();
            Visible = new ReactiveProperty<bool>();
        }
    }
}