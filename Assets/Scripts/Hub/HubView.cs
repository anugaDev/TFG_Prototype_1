using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubView : MonoBehaviour
    {
        [SerializeField] private SoundParameter playerProximity;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetPlayerDistanceToHub(float distance)
        {
            playerProximity.ApplyParameter(distance);
        }
    }
}