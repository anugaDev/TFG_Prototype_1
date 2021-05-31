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

        [SerializeField] private PlayerConfig playerConfig;
        [SerializeField] private ItemConfig itemConfig;
        private void Awake()
        {
            var hubModel = new HubModel();
            var playerModel = new PlayerModel(playerConfig);
            var relicModels = new RelicModel[relicInstaller.GetTotalViews];
            
            for (var i = 0; i < relicModels.Length; i++)
            {
                relicModels[i] = new RelicModel(itemConfig);
            }
            
            var onPlayerMovedCommand = new OnPlayerMovedCommand(hubModel);
            var onRelicTouchingAltar = new OnRelicTouchingAltar(hubModel, playerModel);
            var onRelicPlaced = new OnRelicPlaced(hubModel,playerModel);
            
            
            hubInstaller.Install(hubModel, onRelicPlaced);
            relicInstaller.Install(relicModels, onRelicTouchingAltar);
            playerInstaller.Install(onPlayerMovedCommand, playerModel);
        }
    }
}
