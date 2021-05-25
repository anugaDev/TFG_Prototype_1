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

        public readonly ItemConfig itemConfig;
        
        public RelicModel(ItemConfig _itemConfig)
        {
            isTaken = new ReactiveProperty<bool>();
            isPlaced = new ReactiveProperty<bool>();
            Visible = new ReactiveProperty<bool>();

            itemConfig = _itemConfig;
        }
    }
}