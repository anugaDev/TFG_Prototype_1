using System.Collections;
using System.Collections.Generic;
using Reliquary.Relic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Hub.Stand
{
    public class StandView : MonoBehaviour
    {
        [SerializeField] private Transform light;
        [SerializeField] private RelicView assignedRelic;


        public void SetLightActive(bool _active)
        {
            light.gameObject.SetActive(_active);
        }

        public RelicView GetRelic()
        {
            return assignedRelic;
        }
    }
}