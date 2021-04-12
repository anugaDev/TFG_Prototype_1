using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using Reliquary.Player;
using Reliquary.Relic;
using UnityEngine;

namespace Reliquary
{
    public class MainSceneInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerInstaller playerInstaller;
        [SerializeField] private HubInstaller hubInstaller;
        [SerializeField] private RelicInstaller relicInstaller;
        private void Awake()
        {
            var hubModel = new HubModel();
            
            var playerModel = new PlayerModel();
            
            var relicModels = new RelicModel[relicInstaller.GetTotalViews];
            for (var i = 0; i < relicModels.Length; i++)
            {
                relicModels[i] = new RelicModel();
            }
            
            
            var onPlayerMovedCommand = new OnPlayerMovedCommand(hubModel);
            
            
            hubInstaller.Install(hubModel);
            relicInstaller.Install(relicModels);
            playerInstaller.Install(onPlayerMovedCommand, playerModel);
        }
    }
}
