using System;
using System.Collections.Generic;
using System.Linq;
using FMOD.Studio;
using FMODUnity;
using UniRx;
using UnityEngine;
using Debug = UnityEngine.Debug;
using STOP_MODE = FMOD.Studio.STOP_MODE;
using Reliquary.Sound.Studio;

namespace Reliquary.Sound
{
    public class SoundController : MonoBehaviour
    {
        [SerializeField] private ASoundElement masterTrack;
        [FMODUnity.BankRef] [SerializeField] private string[] studioBanks;


        public const int START_EVENTS_ON_BAR = 1;

        private List<ASoundElement> aSoundElements;
        private static Dictionary<ASoundElement, EventInstance> beatPendingEvents = new Dictionary<ASoundElement, EventInstance>();

        private StudioEventProxy masterTrackProxy;


        private void Start()
        {
            LoadStudioBanks();   
            masterTrack.SetNewEvent();
            masterTrackProxy = new StudioEventProxy(masterTrack.GetEvent());

            masterTrackProxy.eventTimelineInfo.bar.AsObservable().Subscribe(newBar => OnBarChanged(newBar));
            masterTrackProxy.eventTimelineInfo.beat.AsObservable().Subscribe(newBeat => OnBeatChanged(newBeat));

            
            PlayElement(masterTrack);

        }  
        private void LoadStudioBanks()
        {
            foreach (var bankID in studioBanks)
            {
                RuntimeManager.LoadBank(bankID);

                var bank = new Bank();

                RuntimeManager.StudioSystem.getBank(bankID, out bank);

                bank.loadSampleData();

            }
        }
        public void StopAllElements()
        {
            foreach (var element in aSoundElements)
            {
                
            }

        }

        public void ResumeSoundElements(string bankName)
        {

        }

        public static void PlayElement(ASoundElement soundElement)
        {
            var  soundEvent = soundElement.GetEvent();

            if (soundElement.PlayOnBeat)
            {
                beatPendingEvents.Add(soundElement,soundEvent);
            }
            else
            {
                soundEvent.start();
            }
        }

        public void StopElement(ASoundElement soundElement)
        {
            soundElement.StopEvent();
        }

        private void OnBeatChanged(float newBeat)
        {
            
            foreach (var pendingEvent in beatPendingEvents.ToList())
            {
                if (pendingEvent.Key.BeatToPlayOn == newBeat &&
                    masterTrackProxy.eventTimelineInfo.bar.Value == START_EVENTS_ON_BAR)
                {
                    var result = new EventDescription();
                    pendingEvent.Value.start();
                    /*Debug.Log("PENDING EVENT STARTED . Element" + pendingEvent.Key.EventName + " , " +
                              pendingEvent.Value.getDescription(out result));*/

                    RuntimeManager.StudioSystem.update();
                    
                    beatPendingEvents.Remove(pendingEvent.Key);

                }
            }
        }
        

        private void OnBarChanged(float newBar)
        {
            
        }
        private void OnGUI()
        {
            GUILayout.Box(String.Format("Current Beat = {0}, Currentbar = {1}",
                
                masterTrackProxy.eventTimelineInfo.beat.Value,
                masterTrackProxy.eventTimelineInfo.bar.Value
            ));
        }
        
        private void OnDestroy()
        {
            masterTrack.GetEvent().stop(STOP_MODE.IMMEDIATE);
            masterTrack.GetEvent().release();
            masterTrack.GetEvent().setUserData(IntPtr.Zero);
            
	
           masterTrackProxy.OnDestroy();
        }

       
    }
    


}