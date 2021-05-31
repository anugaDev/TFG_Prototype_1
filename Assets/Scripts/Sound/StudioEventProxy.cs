using System;
using UniRx;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using FMOD.Studio;
using UnityEngine;
using UnityEngine.Events;

namespace Reliquary.Sound.Studio
{
   
    public class StudioEventProxy
    {
        [StructLayout(LayoutKind.Sequential)]
        public class EventTimelineInfo
        {
            public EventTimelineInfo()
            {
                bar = new ReactiveProperty<int>();
                beat = new ReactiveProperty<int>();
                marker = new ReactiveProperty<string>();
            }
            public ReactiveProperty<int> bar;
            public ReactiveProperty<int> beat;
            public ReactiveProperty<string> marker;

        }
        
        public EventTimelineInfo eventTimelineInfo;
        
        private GCHandle timelineHandle;
    
        FMOD.Studio.EVENT_CALLBACK eventCallback;
    
        public StudioEventProxy(EventInstance eventInstance)
        {
            eventTimelineInfo = new EventTimelineInfo();
            eventCallback = new FMOD.Studio.EVENT_CALLBACK(StudioBeatEvent);
    
            timelineHandle = GCHandle.Alloc(eventTimelineInfo, GCHandleType.Pinned);
            eventInstance.setUserData(GCHandle.ToIntPtr(timelineHandle));
    
            eventInstance.setCallback(eventCallback, FMOD.Studio.EVENT_CALLBACK_TYPE.ALL);
    
    
        }    
        [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
        private static FMOD.RESULT StudioBeatEvent(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters)
        {
    
            FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(eventInstance);
    
            // Retrieve the user data
            IntPtr timelineInfoPtr;
            FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
    
            if (result != FMOD.RESULT.OK)
            {
                Debug.LogError("Timeline Callback error: " + result);
            }
            else if (timelineInfoPtr != IntPtr.Zero)
            {
                GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
                var timelineInfo = (EventTimelineInfo) timelineHandle.Target;

                switch (type)
                {                    
                    case EVENT_CALLBACK_TYPE.TIMELINE_BEAT:
                        
                        var parameter =
                            (FMOD.Studio.TIMELINE_BEAT_PROPERTIES) Marshal.PtrToStructure(parameters,
                                typeof(FMOD.Studio.TIMELINE_BEAT_PROPERTIES));
    
                        timelineInfo.bar.Value = parameter.bar;
                        timelineInfo.beat.Value = parameter.beat;
                        
                        break;
                    
                    case EVENT_CALLBACK_TYPE.TIMELINE_MARKER:
                        
                        var marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES) Marshal.PtrToStructure(parameters,
                            typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                        //IntPtr namePtr = parameters; 
                        int nameLen = 0;
                        while (Marshal.ReadByte(parameters, nameLen) != 0) ++nameLen;
                        byte[] buffer = new byte[nameLen];
                        Marshal.Copy(parameters, buffer, 0, buffer.Length);
                        string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
                
                        /*if (name == "HIGH")
                        {
                            UnityEngine.Debug.Log("Reached high intensity marker");
                        }*/
                        timelineInfo.marker.Value = marker.name;
                        break;
                }
            }
           
            return FMOD.RESULT.OK;
    
        }
        
        [AOT.MonoPInvokeCallback(typeof(FMOD.Studio.EVENT_CALLBACK))]
        private static FMOD.RESULT StudioMarkerEvent(FMOD.Studio.EVENT_CALLBACK_TYPE type, IntPtr eventInstance, IntPtr parameters)
        {
    
            FMOD.Studio.EventInstance instance = new FMOD.Studio.EventInstance(eventInstance);
    
            // Retrieve the user data
            IntPtr timelineInfoPtr;
            FMOD.RESULT result = instance.getUserData(out timelineInfoPtr);
    
            if (result != FMOD.RESULT.OK)
            {
                Debug.LogError("Timeline Callback error: " + result);
            }
            else if (timelineInfoPtr != IntPtr.Zero)
            {
                var marker = (FMOD.Studio.TIMELINE_MARKER_PROPERTIES) Marshal.PtrToStructure(parameters,
                        typeof(FMOD.Studio.TIMELINE_MARKER_PROPERTIES));
                //IntPtr namePtr = parameters; 
                int nameLen = 0;
                while (Marshal.ReadByte(parameters, nameLen) != 0) ++nameLen;
                byte[] buffer = new byte[nameLen];
                Marshal.Copy(parameters, buffer, 0, buffer.Length);
                string name = Encoding.UTF8.GetString(buffer, 0, nameLen);
                
                GCHandle timelineHandle = GCHandle.FromIntPtr(timelineInfoPtr);
                var timelineInfo = (EventTimelineInfo) timelineHandle.Target;
                
                /*if (name == "HIGH")
                {
                    UnityEngine.Debug.Log("Reached high intensity marker");
                }*/
                timelineInfo.marker.Value = marker.name;

            }

            return FMOD.RESULT.OK;
    
        }
    
        public void OnDestroy()
        {
            if(timelineHandle.IsAllocated)
                timelineHandle.Free();
        }
    }

}
