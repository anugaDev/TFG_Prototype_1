using System;
using System.Collections;
using System.Collections.Generic;
using Reliquary.Sound;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

namespace Reliquary.Stalker
{
    public class StalkerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private ASoundElement loop;
        [SerializeField] private SoundParameter enemyStatesLabeled;
        [SerializeField] private float completedPathOffset;
        [SerializeField] private GameObject visionRadius;
        private float gizmoRadius;
        private StalkerController controller;

        private void Awake()
        {
            controller?.Spawn();
        }

        public StalkerController Controller
        {
            set => controller = value;
        }
        
        public void SetNavMeshDestination(Transform _destination)
        {
            navMeshAgent.SetDestination(_destination.position);
        }

        public void SetCurrentSpeed(float _currentSpeed)
        {
            navMeshAgent.speed = _currentSpeed;
        }

        public bool IsCurrentPathCompleted()
        {
            /*Debug.Log("Distance from destination "+
                      Vector3.Distance(transform.position, navMeshAgent.destination));*/
            
            return Vector3.Distance(transform.position, navMeshAgent.destination) <= completedPathOffset
                || navMeshAgent.pathPending;
        }
        
        public bool TargetVisible(GameObject _target)
        {
            RaycastHit hit;        
            if ((Physics.Raycast(transform.position,(_target.transform.position - transform.position).normalized ,out hit)))
            {
                return hit.collider.tag == _target.tag;
            }

            return false;
        }

        public void SetVisibleDetectionRadius(float _radius)
        {
            gizmoRadius = _radius;
            visionRadius.transform.localScale = new Vector3(
                _radius,
                visionRadius.transform.localScale.y,
                _radius);
        }
        public IEnumerator LookArround()
        {
            yield return new WaitForSeconds(2.0f);
            
            controller.LookArroundFinished();
        }

        public void StartLoop()
        {
            SoundController.PlayElement(loop);
        }

        public void SetNewState(EEnemyStates _newState)
        {
            enemyStatesLabeled.ApplyParameter((float)_newState);
            enemyStatesLabeled.DebugParameter();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(navMeshAgent.destination, 1.0f);
        }
        
        private void Update()
        {
            controller.StateUpdate();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                controller.PlayerTouched();
            }
            else if (other.tag == "Hub")
            {
                controller.HubTouched();
            }
        }

    }
}
