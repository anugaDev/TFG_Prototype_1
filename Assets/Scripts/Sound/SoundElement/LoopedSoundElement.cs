using System;
using System.Collections;
using System.Collections.Generic;
using FMOD;
using FMOD.Studio;
using Reliquary.Sound;
using UnityEngine;
using STOP_MODE = FMODUnity.STOP_MODE;

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

    public override void StopEvent()
    {
        studioEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }

    public override bool IsPlaying()
    {
        if (!uniqueEventCreated)
            return false;

        var result = PLAYBACK_STATE.STOPPED;
        
        studioEvent.getPlaybackState(out result);

        if (result == PLAYBACK_STATE.PLAYING || result == PLAYBACK_STATE.STARTING)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
