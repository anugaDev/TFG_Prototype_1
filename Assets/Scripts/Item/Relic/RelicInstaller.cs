using System;
using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicInstaller : MonoBehaviour
    {
        [SerializeField] private RelicView[] views;

        public int GetTotalViews => views.Length;

        public void Install(RelicModel [] relicModels, OnRelicTouchingAltar _onRelicTouchingAltar)
        {
            foreach (var model in relicModels)
            {
                new RelicController(
                    model, 
                    views[Array.IndexOf(relicModels, model)],
                    _onRelicTouchingAltar
                    );
            }
        }
    }
}
