using System.Collections;
using System.Collections.Generic;
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
            movement.onPlayerMovedCommand = _onPlayerMovedCommand;   
        }
    }
}