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

        [SerializeField] private PlayerMovement movement;

        
        public void Install(OnPlayerMovedCommand _onPlayerMovedCommand, PlayerModel playerModel)
        {
            var onItemPickedUp = new OnItemPickedUp(playerModel);
            var onItemDropped = new OnItemDropped(playerModel);
            
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