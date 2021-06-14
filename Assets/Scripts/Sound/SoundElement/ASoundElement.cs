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

        [SerializeField] protected SoundParameter[] soundParameters;

        public void SetDefaultParameters()
        {
            foreach (var parameter in soundParameters)
            {
                parameter.SetDefault();
            }
        }

        public bool PlayOnBeat => playOnBeat;
        public int BeatToPlayOn => beatToPlayOn;
        
        public abstract EventInstance GetEvent();

        public abstract void SetNewEvent();

        public abstract void StopEvent();
        
        public abstract bool IsPlaying();

        
        public string EventName => eventName;

        private void OnDisable()
        {
            StopEvent();
        }

        private void Update()
        {
            GetEvent().set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        }

    }
}