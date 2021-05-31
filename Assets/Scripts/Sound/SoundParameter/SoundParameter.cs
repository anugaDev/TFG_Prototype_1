using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Sound
{
    public class SoundParameter : MonoBehaviour
    {
        [SerializeField] protected ASoundElement[] soundElements;
        [FMODUnity.ParamRef] [SerializeField] protected string parameterName;        
        [SerializeField] protected float defaultValue;

        public void SetDefault()
        {
            foreach (var soundElement in soundElements)
            {
                soundElement.GetEvent().setParameterByName(parameterName, defaultValue);
            }
        }

        public void ApplyParameter(float value)
        {
            foreach (var soundElement in soundElements)
            {
                var eventInstance = soundElement.GetEvent();
            
                eventInstance.setParameterByName(parameterName, value);
            }

        }

        public void DebugParameter()
        {
            foreach (var soundElement in soundElements)
            {
                var currentvalue = new float();
                var eventInstance = soundElement.GetEvent();
                var result = eventInstance.getParameterByName(parameterName, out currentvalue);

                Debug.Log(parameterName + " : " + result + " " + currentvalue);
            }


        }

    }
}
