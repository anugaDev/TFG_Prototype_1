using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub.Stand;
using Reliquary.Relic;
using Reliquary.Sound;
using Reliquary.Utils;
using UnityEngine;

namespace Reliquary.Hub
{
    public class HubView : MonoBehaviour
    {
        [SerializeField] private SoundParameter playerProximity;
        [SerializeField] private SoundParameter gameStatesLabeled;
        [SerializeField] private ASoundElement masterMusic;
        [SerializeField] private ASoundElement relicReturned;
        [SerializeField] private StandView[] stands;
        [SerializeField] private SetObjectsActive relicGates;
        [SerializeField] private GameObject victoryTrigger;
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

        public void SetNewStateParameter(EGameStates newState)
        {
            gameStatesLabeled.DebugParameter();
            gameStatesLabeled.ApplyParameter((float) newState);
        }

        private StandView GetStand(RelicView _relic)
        {
            foreach (var stand in stands)
            {
                if (stand.GetRelic() == _relic)
                {
                    return stand;
                }
            }

            return null;
        } 

        public IEnumerator PlacingRelicAnimation(float _waitTime, RelicView _relic)
        {
            yield return new WaitForSeconds(_waitTime);
            controller.OnRelicPlaced();
            GetStand(_relic).SetLightActive(true);            
        }

        public void CloseGates()
        {
            relicGates.SetActive(true);
        }

        public void SetVictoryTriggerActive()
        {
            victoryTrigger.SetActive(true);
        }
    }
}