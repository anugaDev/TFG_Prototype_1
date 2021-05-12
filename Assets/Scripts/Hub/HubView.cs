using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubView : MonoBehaviour
    {
        [SerializeField] private SoundParameter playerProximity;
    
        public void SetPlayerDistanceToHub(float distance)
        {
            playerProximity.ApplyParameter(distance);
        }
    }
}