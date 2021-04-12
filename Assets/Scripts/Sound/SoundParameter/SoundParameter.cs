using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Sound
{
    public class SoundParameter : MonoBehaviour
    {
        [SerializeField] protected ASoundElement soundElement;
        [SerializeField] protected string parameterName;

        [SerializeField] protected float defaultValue;

        public void SetDefault()
        {
            soundElement.GetEvent().setParameterByName(parameterName, defaultValue);
        }

        public void ApplyParameter(float value)
        {
            var eventInstance = soundElement.GetEvent();

            eventInstance.setParameterByName(parameterName, value);
        }

    }
}
