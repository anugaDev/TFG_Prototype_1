using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Relic
{
    public class RelicView : MonoBehaviour
    {
        [SerializeField] private ASoundElement loop;
        [SerializeField] private SoundParameter taken;


        private void Awake()
        {
            SoundController.PlayElement(loop);
        }

        // Start is called before the first frame update
        void Start()
        {
            

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PickedUp()
        {
            taken.ApplyParameter(1.0f);
        }
    }
}
