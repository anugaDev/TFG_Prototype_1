using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubController
    {
        private HubModel hubModel;
        private HubView hubView;

        public HubController(HubModel _hubModel, HubView _hubView)
        {
            hubModel = _hubModel;
            hubView = _hubView;
            
            hubModel.playerDistance.AsObservable().Subscribe(distance =>
            {
                hubView.SetPlayerDistanceToHub(distance);
            } );
        }
    }
}