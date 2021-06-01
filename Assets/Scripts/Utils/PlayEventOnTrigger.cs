using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Reliquary.Utils
{
    public class PlayEventOnTrigger : MonoBehaviour
    {
        [SerializeField] private UnityEvent triggeredEvent;

        private void OnTriggerEnter(Collider other)
        {
            triggeredEvent.Invoke();
        }
    }
}
