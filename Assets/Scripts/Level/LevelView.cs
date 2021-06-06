using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Level
{
    public class LevelView : MonoBehaviour
    {
        [SerializeField] private SoundParameter gameStatesLabeled;
        
        public void SetNewStateParameter(EGameStates newState)
        {
            gameStatesLabeled.ApplyParameter((float) newState);
        }

    }
}
