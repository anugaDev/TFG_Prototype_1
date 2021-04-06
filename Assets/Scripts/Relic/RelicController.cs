using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicController
    {
        private readonly RelicModel relicModel;
        private readonly RelicView relicView;
        
        public RelicController(RelicModel _relicModel, RelicView _relicView)
        {
            relicModel = _relicModel;
            relicView = _relicView;

        }
    }
}

