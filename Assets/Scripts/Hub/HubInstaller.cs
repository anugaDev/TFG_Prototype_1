using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubInstaller : MonoBehaviour
    {
        [SerializeField] private HubView view;
        
        public void Install(HubModel _model)
        {
            new HubController
            (
                _model,
                view
            );
        }
    }
}

