using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using Reliquary.Sound;
using UniRx;
using UnityEngine;

namespace Reliquary.Player
{

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private ASoundElement pickUp;
        [SerializeField] private ASoundElement drop;
        [SerializeField] private ASoundElement steps;
        [SerializeField] private KeyCode pickUpItem = KeyCode.Space;
        [SerializeField] private KeyCode pray = KeyCode.LeftControl;
        [SerializeField] private Collider detectionTrigger;
        [SerializeField] private LayerMask detectionLayers;

        [SerializeField] private Rigidbody rigidBody;
        [SerializeField] private Transform carry;

        private PlayerController controller;


        private Vector3 direction;

        private float currentRotationSpeed;
        //private List<GameObject> collidingItems = new List<GameObject>();
        
        public PlayerController Controller
        {
            get => controller;
            set => controller = value;
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = Vector3.zero;
            UpdateMovement();

            if (direction != Vector3.zero)
                RotateTowardsDirection();
        }

        private void Update()
        {
            if (Input.GetKeyDown(pickUpItem))
            {
                
            
                if (controller.IsDead())
                {
                    controller.SetAlive();
                }
                else
                {
                    if (controller.IsCarryingItem())
                    {
                        controller.Dropitem();
                    }
                    else
                    {
                        controller.PickUpItem(GetClosestItem());
                    }
                }
            }
            
            controller.SetSneaking(Input.GetKey(pray));
        }

        private void UpdateMovement()
        {
            var movement = Vector3.zero;

            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");

            
            var _currentSpeed = controller.GetCurrentSpeed(movement);

            if (_currentSpeed <= 0)
                return;
            
            direction = movement;

            movement = Vector3.ClampMagnitude(movement, 1f) * (_currentSpeed * Time.fixedDeltaTime);
            rigidBody.velocity = movement;

            controller.PlayerMoved(transform.position);
        }

        private void RotateTowardsDirection()
        {
            currentRotationSpeed = controller.GetCurrentRotationSpeed();
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction),
                Time.fixedDeltaTime * currentRotationSpeed);
        }

        private GameObject GetClosestItem()
        {
            var colliders = Physics.OverlapBox(detectionTrigger.gameObject.transform.position,
                detectionTrigger.bounds.size / 2, Quaternion.identity, detectionLayers);

            if (colliders.Length > 0)
            {

                var closestItem = colliders.First();
                foreach (var item in colliders)
                {
                    if (Vector3.Distance(item.transform.position, transform.position) <
                        Vector3.Distance(closestItem.transform.position, transform.position))
                    {
                        closestItem = item;
                    }
                }

                return closestItem.gameObject;

            }
            else
            {
                Debug.Log("objects not found");
            }
            
            return null;
        }

        public void PlayPickUpSound()
        {
            SoundController.PlayElement(pickUp);
        }

        public void PlayDropSound()
        {
            SoundController.PlayElement(drop);
        }

        public void PlaySteps()
        {
            if (!steps.IsPlaying())
            {
                SoundController.PlayElement(steps);
            }
        }
        
        public void StopSteps()
        {
            SoundController.StopElement(steps);
        }
        
        public void CarryItem(Transform itemTransform)
        {
            itemTransform.SetParent(carry.transform);
            itemTransform.position = carry.position;

        }
    }
}


