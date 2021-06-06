using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Reliquary.Stalker
{
    [CreateAssetMenu(fileName = "StalkerConfiguration", menuName = "Config/StalkerConfiguration")]
    public class StalkerConfiguration : ScriptableObject
    {
        public float wanderSpeed = 2f;
        public float patrolSpeed = 2.5f;
        public float chaseSpeed = 5f;
        public float fleeSpeed = 10f;

        public float maxChasePlayerDistance = 10f;
        public float playerDetectionRadius = 10f;
    }
}