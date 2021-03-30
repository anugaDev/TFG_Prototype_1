using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FMOD;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Reliquary.Sound
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private ASoundElement masterTrack;
        private List<ASoundElement> aSoundElements;
        private string[] busList;

        private Dictionary<ASoundElement, EventInstance> pendingEvents;

        private void Awake()
        {
          SceneMasterTrackSetUp();
        }

        public void StopAllElements()
        {
            

        }

        public void ResumeSoundElements()
        {

        }

        public void PlayElement(ASoundElement soundElement)
        {
            var  soundEvent = soundElement.GetEvent();

            if (soundElement.playOnTempo)
            {
                pendingEvents.Add(soundElement,soundEvent);
                
                
            }
            else
            {
                soundEvent.start();
            }
        }

        public void StopElement(ASoundElement aSoundElement)
        {

        }

        private void SceneMasterTrackSetUp()
        {
            masterTrack.SetNewEvent();
            PlayElement(masterTrack);
            
            var cb = new FMOD.Studio.EVENT_CALLBACK(StudioBeatCallback);
            masterTrack.GetEvent().setCallback(cb, FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT);
        }
        
        private FMOD.RESULT StudioBeatCallback(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters)
        {
            Debug.Log("BEAT CALLBACK");
            if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_MARKER)
            {
                FMOD.Studio.TIMELINE_MARKER_PROPERTIES marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                //IntPtr namePtr = parameters; 
                int nameLen = 0;
                while (Marshal.ReadByte(parameters, nameLen) != 0) ++nameLen;
                byte[] buffer = new byte[nameLen];
                Marshal.Copy(parameters, buffer, 0, buffer.Length);
                string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
                if (name == "HIGH")
                {
                    UnityEngine.Debug.Log("Reached high intensity marker");
                }
            }
            if (type == FMOD.Studio.EVENT_CALLBACK_TYPE.TIMELINE_BEAT)
            {
                FMOD.Studio.TIMELINE_BEAT_PROPERTIES beat = (FMOD.Studio.TIMELINE_BEAT_PROPERTIES)Marshal.PtrToStructure(parameters, typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
            }
            return FMOD.RESULT.OK;
        }
        
        
    }
    


}