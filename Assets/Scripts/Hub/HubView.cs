using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubView : MonoBehaviour
    {
        [SerializeField] private SoundParameter playerProximity;
        [SerializeField] private SoundParameter gameStatesLabeled;
        [SerializeField] private ASoundElement masterMusic;
        [SerializeField] private ASoundElement relicReturned;
        private HubController controller;

        public HubController Controller
        {
            get => controller;
            set => controller = value;
        }
        public void SetPlayerDistanceToHub(float distance)
        {
            playerProximity.ApplyParameter(distance);
        }

        public void PlayRelicReturnedSound()
        {
            SoundController.PlayElement(relicReturned);
        }

        public void SetNewStateParameter(HubModel.GameState newState)
        {
            gameStatesLabeled.DebugParameter();
            gameStatesLabeled.ApplyParameter((float) newState);
        }

        public IEnumerator PlacingRelicAnimation(float _waitTime)
        {
            yield return new WaitForSeconds(_waitTime);
            controller.OnRelicPlaced();

        }
    }
}