using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Player
{
    [CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Config/PlayerConfiguration")]
    public class PlayerConfiguration : ScriptableObject
    {
        public float defaultSpeed;
        public float praySpeed;
        public float carryingSpeed;
        public float rotationSpeed;
    }
}
