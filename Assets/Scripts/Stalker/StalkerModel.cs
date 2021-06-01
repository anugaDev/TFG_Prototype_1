using System.Collections;
using System.Collections.Generic;
using Reliquary.Maze;
using UniRx;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class StalkerModel
    {
        public readonly ReactiveProperty<EStalkerStates> currentState;
        public readonly ReactiveProperty<bool> isLooking;
        private GameObject currentTarget;
        private float wanderSpeed;
        private float patrolSpeed;
        private float chaseSpeed;
        private float fleeSpeed;

        private float maxChasePlayerDistance;
        private MazeConfiguration mazeConfiguration;
        

        public float WanderSpeed => wanderSpeed;
        public float PatrolSpeed => patrolSpeed;
        public float ChaseSpeed => chaseSpeed;
        public float FleeSpeed => fleeSpeed;
        
        public StalkerModel()
        {
            currentState = new ReactiveProperty<EStalkerStates>(EStalkerStates.Wander);
        }

        public GameObject GetCurrentTarget()
        {
            return currentTarget;
        }

        public bool PlayerEscaped(Vector3 currentPosition)
        {
            return Vector3.Distance(currentPosition, currentTarget.transform.position) >= maxChasePlayerDistance;
        }

        public Transform GetFleeDestination()
        {
            return mazeConfiguration.fleePoint.transform;
        }

        public Transform GetRandomWanderDestination()
        {
            return mazeConfiguration.wanderPoints[Random.Range(0, mazeConfiguration.wanderPoints.Length)];
        }
    }
}
