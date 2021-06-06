using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubController
    {
        private HubModel model;
        private HubView view;
        private OnRelicPlaced onRelicPlaced;

        public HubController(HubModel _model, HubView _view, OnRelicPlaced _onRelicPlaced)
        {
            model = _model;
            view = _view;
            onRelicPlaced = _onRelicPlaced;

            view.Controller = this;
            
            model.playerProximity.AsObservable().Subscribe(distance =>
            {
                view.SetPlayerDistanceToHub(distance);
            } );
            model.acquiredRelics.ObserveAdd().Subscribe(relic =>
            {
                view.SetNewStateParameter(EGameStates.Powering);
                view.StartCoroutine(view.PlacingRelicAnimation(model.GetPlacingTime(), relic.Value));

            });
        }

        public void OnRelicPlaced()
        {
            view.PlayRelicReturnedSound();

            onRelicPlaced.Execute();

            if (model.AllRelicsReturned())
            {
                view.CloseGates();
                view.SetNewStateParameter(EGameStates.Ending);
            }
            else
            {
                view.SetNewStateParameter(EGameStates.Walking);
            }
        }
    }
}