using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicView : BaseItemView
    {
        private RelicController controller;

        public RelicController Controller
        {
            set => controller = value;
        }

        
        
        [SerializeField] private ASoundElement loop;
        [SerializeField] private SoundParameter taken;
        
        private void Awake()
        {
            SoundController.PlayElement(loop);
        }
        
        public override void PickUp()
        {
            controller.PickUp();
            taken.ApplyParameter(1.0f);
        }
        public override void Drop()
        {
            taken.ApplyParameter(0.0f);
        }
    }
    
}
