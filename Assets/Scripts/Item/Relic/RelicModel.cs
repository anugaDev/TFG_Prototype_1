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

        public readonly ItemConfiguration ItemConfiguration;
        
        public RelicModel(ItemConfiguration itemConfiguration)
        {
            isTaken = new ReactiveProperty<bool>(false);
            isPlaced = new ReactiveProperty<bool>(false);
            Visible = new ReactiveProperty<bool>(false);

            ItemConfiguration = itemConfiguration;
        }
    }
}