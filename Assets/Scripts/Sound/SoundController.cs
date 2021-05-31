using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using FMOD;
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
        [SerializeField] private string[] bankPaths;


        public const int START_EVENTS_ON_BAR = 9;

        [SerializeField ]private ASoundElement [] beatLoopElements;
        
        private static Dictionary<ASoundElement, EventInstance> beatPendingEvents = new Dictionary<ASoundElement, EventInstance>();

        private StudioEventProxy masterTrackProxy;

        public enum TimelineMarkers
        {
            EnableBeatEvents,
            WalkingLoop,
            Powering
            
        }

        private void Start()
        {
            StartCoroutine(LoadStudioBanks());   
        }  
        private IEnumerator LoadStudioBanks()
        {
            foreach (var bankID in studioBanks)
            {
                var result = FMOD.RESULT.OK;
                RuntimeManager.LoadBank(bankID, true);

                var bank = new Bank();

                Debug.Log("loading");

               RuntimeManager.StudioSystem.getBank(bankID, out bank);

              //  var result = bank.loadSampleData();
              //  Debug.Log(bankID + " : " + result);
                
                var loadingState = LOADING_STATE.LOADING;
                while (RuntimeManager.AnyBankLoading())
                {
                    
                    yield return null;
                    /*
                    bank.getSampleLoadingState(out loadingState);
                    Debug.Log(bankID + " "+ loadingState);*/
                    Debug.Log("loading");
                }

            }
            masterTrack.SetNewEvent();
            masterTrackProxy = new StudioEventProxy(masterTrack.GetEvent());

            masterTrackProxy.eventTimelineInfo.bar.AsObservable().Subscribe(newBar => OnBarChanged(newBar));
            masterTrackProxy.eventTimelineInfo.beat.AsObservable().Subscribe(newBeat => OnBeatChanged(newBeat));
            masterTrackProxy.eventTimelineInfo.marker.AsObservable()
                .Subscribe(marker => OnCurrentMarkerChanged(marker));
            
            PlayElement(masterTrack);

        }
        public void StopBeatElements()
        {
            foreach (var element in beatLoopElements)
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
                if ((pendingEvent.Key.BeatToPlayOn == newBeat &&
                    masterTrackProxy.eventTimelineInfo.bar.Value == START_EVENTS_ON_BAR)
                    || pendingEvent.Key.BeatToPlayOn == 0)
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

        private void OnCurrentMarkerChanged(string marker)
        {
            var markerEnum = TimelineMarkers.WalkingLoop;
            var _isParsed = TimelineMarkers.TryParse(marker, out markerEnum);

            if(_isParsed)
            {
                switch (markerEnum)
                {
                    case TimelineMarkers.EnableBeatEvents:
                        Debug.Log("EnableBeatElements");
                        OnEnableBeatElements();
                        break;
                    case TimelineMarkers.WalkingLoop:
                        break;
                    case TimelineMarkers.Powering:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            else
            {
                Debug.LogError("MARKER NOT FOUND : "+ marker);
            }
        }

        private void OnEnableBeatElements()
        {
            foreach (var beatElement in beatLoopElements)
            {
                beatElement.gameObject.SetActive(true);
            }
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