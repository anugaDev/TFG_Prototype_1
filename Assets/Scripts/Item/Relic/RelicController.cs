using System.Collections;
using System.Collections.Generic;
using UniRx;
using Reliquary.Hub;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicController
    {
        private readonly RelicModel model;
        private readonly RelicView view;
        private readonly OnRelicTouchingAltar onRelicTouchingAltar;
        
        public RelicController(RelicModel _model, RelicView _view, OnRelicTouchingAltar _onRelicTouchingAltar)
        {
            model = _model;
            view = _view;
            onRelicTouchingAltar = _onRelicTouchingAltar;


            view.Controller = this;

        }

        public void PickUp()
        {
            model.isTaken.Value = true;
            view.gameObject.layer = model.itemConfig.NonInteractableLayer;
        }

        public void Drop()
        {
            view.gameObject.layer = model.itemConfig.InteractableLayer;
        }

        public void IsAltarTouched(Transform objectTransform)
        {
            if (view.tag == objectTransform.tag)
            {
                onRelicTouchingAltar.Execute(view, objectTransform, model);
                view.PlayPlacedSound();
            }
        }
        
    }
}

