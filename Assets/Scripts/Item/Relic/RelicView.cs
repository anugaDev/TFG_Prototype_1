using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicView : BaseItemView
    {

        [SerializeField] private ASoundElement loop;
        [SerializeField] private SoundParameter taken;
        [SerializeField] private Collider collider;
        private RelicController controller;

        public RelicController Controller
        {
            set => controller = value;
        }
        private void Awake()
        {
            SoundController.PlayElement(loop);
        }
        
        public override void PickUp()
        {
            controller.PickUp();
            collider.isTrigger = true;
            taken.ApplyParameter(1.0f);
        }
        public override void Drop()
        {
            controller.Drop();
            collider.isTrigger = false;
            taken.ApplyParameter(0.0f);
        }
        private void OnTriggerEnter(Collider other)
        {
            controller.IsAltarTouched(other.transform.tag);
        }
    }

}
