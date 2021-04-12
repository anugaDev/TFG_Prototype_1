using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicModel
    {
        public ReactiveProperty<bool> isTaken;
        public ReactiveProperty<bool> isPlaced;
        
        public RelicModel()
        {
            
        }
    }
}