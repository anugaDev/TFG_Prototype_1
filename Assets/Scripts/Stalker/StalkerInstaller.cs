using System.Collections;
using System.Collections.Generic;
using Reliquary.Level;
using Reliquary.Player;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class StalkerInstaller : MonoBehaviour
    {
        [SerializeField] private StalkerView view;
        public void Install(OnPlayerTouchedCommand onPlayerTouchedCommand, PlayerModel _playerModel, StalkerModel _model)
        {

            new StalkerController(_model, view, onPlayerTouchedCommand);
        }
    }
}