using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Sound
{

    public class OneShootSoundElement : ASoundElement
    {
        private List<EventInstance> studioEventsPool;
        
        public override EventInstance GetEvent()
        {
            return GetEventFromPool();
        }
        private EventInstance GetEventFromPool()
        {
            SetNewEvent();
            return studioEventsPool[studioEventsPool.Count-1];
        }
        public override void SetNewEvent()
        {
            studioEventsPool.Add(FMODUnity.RuntimeManager.CreateInstance(eventName));

        }


    }
}
