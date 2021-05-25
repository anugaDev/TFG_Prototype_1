﻿using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicController
    {
        private readonly RelicModel model;
        private readonly RelicView view;
        
        public RelicController(RelicModel _model, RelicView _view)
        {
            model = _model;
            view = _view;

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

        public void IsAltarTouched(string objectID)
        {
            Debug.Log("ID : " + objectID);
            if (view.tag == objectID)
            {
                Debug.Log("My altar touched");
            }
        }
        
    }
}

