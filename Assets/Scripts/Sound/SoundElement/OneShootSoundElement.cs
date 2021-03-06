using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Sound
{

    public class OneShootSoundElement : ASoundElement
    {
        private List<EventInstance> eventPool = new List<EventInstance>();
        
        public override EventInstance GetEvent()
        {
            return GetEventFromPool();
        }
        
        public override void SetNewEvent()
        {
            
            eventPool.Add(FMODUnity.RuntimeManager.CreateInstance(eventName));

        }

        public override void StopEvent()
        {
            var eventPlaybackState = PLAYBACK_STATE.PLAYING;
            foreach (var eventInstance in eventPool)
            {
                eventInstance.getPlaybackState(out eventPlaybackState);

                if (eventPlaybackState == PLAYBACK_STATE.PLAYING)
                {
                    eventInstance.stop(STOP_MODE.IMMEDIATE);
                }
            }
        }

        public override bool IsPlaying()
        {
            return false;
        }

        private EventInstance GetEventFromPool()
        {
            var eventPlaybackState = PLAYBACK_STATE.PLAYING;
            foreach (var eventInstance in eventPool)
            {
                eventInstance.getPlaybackState(out eventPlaybackState);
                
                if (eventPlaybackState == PLAYBACK_STATE.STOPPED || eventPlaybackState == PLAYBACK_STATE.STOPPING)
                {
                    return eventInstance;
                }
            }
            
            SetNewEvent();
            return eventPool[eventPool.Count-1];
        }
        
    }
}
