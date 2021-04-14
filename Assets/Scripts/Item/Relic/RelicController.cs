using System.Collections;
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
        }
        
    }
}

