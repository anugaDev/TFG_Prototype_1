using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicModel
    {
        private bool isTaken;
        private bool isPlaced;

        public bool IsTaken
        {
            get => isTaken;
            set => isTaken = value;
        }

        public bool IsPlaced
        {
            get => isPlaced;
            set => isPlaced = value;
        }

        public RelicModel()
        {
            
        }
    }
}