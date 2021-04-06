using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;

namespace Reliquary.Sound
{

    public abstract class ASoundElement : MonoBehaviour
    {
        [FMODUnity.EventRef] [SerializeField] protected string eventName;
        [FMODUnity.BankRef] [SerializeField] protected string eventBank;


        [SerializeField] private bool playOnBeat;
        [SerializeField] private int beatToPlayOn;

        public bool PlayOnBeat => playOnBeat;
        public int BeatToPlayOn => beatToPlayOn;
        
        public abstract EventInstance GetEvent();

        public abstract void SetNewEvent();
        
        public string EventName => eventName;
    }
}