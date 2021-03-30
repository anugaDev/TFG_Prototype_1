using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using Reliquary.Sound;
using UnityEngine;

public class LoopedSoundElement : ASoundElement
{
    private EventInstance studioEvent;

    private void Awake()
    {
        SetNewEvent();       
    }

    public override EventInstance GetEvent()
    {
        return studioEvent;
    }

    public override void SetNewEvent()
    {
        studioEvent = FMODUnity.RuntimeManager.CreateInstance(eventName);

    }
}
