using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Reliquary.Stalker
{
    public class StalkerView : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private float completedPathOffset;
        private StalkerController controller;

        public StalkerController Controller
        {
            get => controller;
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
            return Vector3.Distance(transform.position, navMeshAgent.destination) <= completedPathOffset
                || navMeshAgent.pathPending;
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

        public IEnumerator LookArround()
        {
            yield return new WaitForSeconds(2.0f);
        }
    }
}
