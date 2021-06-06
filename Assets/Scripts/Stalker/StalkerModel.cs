using System.Collections;
using System.Collections.Generic;
using Reliquary.Level;
using Reliquary.Player;
using UniRx;
using UnityEngine;

namespace Reliquary.Stalker
{
    public class StalkerModel
    {
        public readonly ReactiveProperty<EEnemyStates> currentState;
        public readonly ReactiveProperty<bool> isLooking;
        private PlayerModel target;

        private StalkerConfiguration configuration;
        private LevelConfiguration levelConfiguration;
        

        public float WanderSpeed => configuration.wanderSpeed;
        public float PatrolSpeed => configuration.patrolSpeed;
        public float ChaseSpeed => configuration.chaseSpeed;
        public float FleeSpeed => configuration.fleeSpeed;
        
        public StalkerModel(LevelConfiguration _levelConfiguration, StalkerConfiguration _configuration, PlayerModel _target)
        {
            currentState = new ReactiveProperty<EEnemyStates>(EEnemyStates.Wander);
            isLooking = new ReactiveProperty<bool>();
            target = _target;
            levelConfiguration = _levelConfiguration;
            configuration = _configuration;
        }

        public GameObject GetCurrentTarget()
        {
            return target.GetAvatar();
        }

        public bool TargetEscaped(Vector3 currentPosition)
        {
            return Vector3.Distance(currentPosition, target.GetAvatar().transform.position) >= configuration.maxChasePlayerDistance;
        }

        public Transform GetFleeDestination()
        {
            return levelConfiguration.fleePoint.transform;
        }

        public Transform GetRandomWanderDestination()
        {
            return levelConfiguration.wanderPoints[Random.Range(0, levelConfiguration.wanderPoints.Length)];
        }

        public bool HearingTarget()
        {
            if (target.IsCarryingItem())
            {
                return true;
            }
            else if (target.currentSpeed > 0)
            {
                return !target.isPraying;
            }
            return false;
        }

        public bool IsTargetDead()
        {
            return target.isDead.Value;
        }

        public float GetDetectionRadius()
        {
            return configuration.playerDetectionRadius;
        }
    }
}
