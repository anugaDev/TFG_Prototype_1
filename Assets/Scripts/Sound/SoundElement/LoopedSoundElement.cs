using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using Reliquary.Sound;
using UnityEngine;

public class LoopedSoundElement : ASoundElement
{
    private EventInstance studioEvent;
    private bool uniqueEventCreated = false;

    private void Awake()
    {
    }

    public override EventInstance GetEvent()
    {
        if(!uniqueEventCreated)
            SetNewEvent();
        
        return studioEvent;
    }

    public override void SetNewEvent()
    {
        if (uniqueEventCreated)
            return;
        
        studioEvent = FMODUnity.RuntimeManager.CreateInstance(eventName);
        uniqueEventCreated = true;
        
        
        FMODUnity.RuntimeManager.StudioSystem.update();

        studioEvent.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
    }
}
