using System.Collections;
using System.Collections.Generic;
using Reliquary.Hub;
using Reliquary.Level;
using Reliquary.Player;
using Reliquary.Relic;
using Reliquary.Stalker;
using UnityEngine;

namespace Reliquary
{
    public class GameSceneInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerInstaller playerInstaller;
        [SerializeField] private HubInstaller hubInstaller;
        [SerializeField] private RelicInstaller relicInstaller;
        [SerializeField] private LevelInstaller[] levelInstallers;

        [SerializeField] private PlayerConfiguration playerConfiguration;
        [SerializeField] private ItemConfiguration itemConfiguration;
        private void Awake()
        {
            var hubModel = new HubModel();
            var playerModel = new PlayerModel(playerConfiguration);
            var relicModels = new RelicModel[relicInstaller.GetTotalViews];
            
            for (var i = 0; i < relicModels.Length; i++)
            {
                relicModels[i] = new RelicModel(itemConfiguration);
            }
            
            var onPlayerMovedCommand = new OnPlayerMovedCommand(hubModel);
            var onPlayerTouched = new OnPlayerTouchedCommand(playerModel, playerInstaller.GetPlayerView());
            var onRelicTouchingAltar = new OnRelicTouchingAltar(hubModel, playerModel);
            var onRelicPlaced = new OnPlayerRelicPlacingEnded(playerModel);
            
            
            hubInstaller.Install(hubModel, onRelicPlaced);
            relicInstaller.Install(relicModels, onRelicTouchingAltar);
            playerInstaller.Install(onPlayerMovedCommand, playerModel);

            foreach (var levelInstaller in levelInstallers)
            {
                levelInstaller.Install(onPlayerTouched, playerModel);
            }
        }
    }
}
