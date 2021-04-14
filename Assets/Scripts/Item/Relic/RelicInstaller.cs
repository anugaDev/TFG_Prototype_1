using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicInstaller : MonoBehaviour
    {
        [SerializeField] private RelicView[] views;

        public int GetTotalViews => views.Length;

        public void Install(RelicModel [] relicModels)
        {
            foreach (var model in relicModels)
            {
                new RelicController(
                    model, 
                    views[Array.IndexOf(relicModels, model)]
                    );
            }
        }
    }
}
