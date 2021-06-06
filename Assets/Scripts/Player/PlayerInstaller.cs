using System.Collections;
using System.Collections.Generic;
using Reliquary.Item;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Player
{
    public class PlayerInstaller : MonoBehaviour
    {
        [SerializeField] private PlayerView view;
        
        public void Install(OnPlayerMovedCommand _onPlayerMovedCommand, PlayerModel playerModel)
        {
            var onItemPickedUp = new OnItemPickedUpCommand(playerModel);
            var onItemDropped = new OnItemDroppedCommand(playerModel);
            
            new PlayerController(
                view,
                playerModel,
                _onPlayerMovedCommand,
                onItemPickedUp,
                onItemDropped
            );
        }
    }
}