using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

namespace Reliquary.Sound
{

    public abstract class ASoundElement : MonoBehaviour
    {
        [SerializeField] protected string eventName;
        [SerializeField] protected string eventBus;
        [SerializeField] public readonly bool playOnTempo;
        public abstract EventInstance GetEvent();

        public abstract void SetNewEvent();
    }
}