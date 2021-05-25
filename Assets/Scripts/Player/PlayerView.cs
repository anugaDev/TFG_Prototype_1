using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using UniRx;
using UnityEngine;

namespace Reliquary.Player
{

    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private KeyCode pickUpItem = KeyCode.Space;
        [SerializeField] private Collider detectionTrigger;
        [SerializeField] private LayerMask detectionLayers;

        [SerializeField] private Rigidbody rigidBody;

        private PlayerController controller;


        private Vector3 direction;
        private float currentSpeed;

        private float currentRotationSpeed;
        //private List<GameObject> collidingItems = new List<GameObject>();


        public PlayerController Controller
        {
            set => controller = value;
        }

        private void FixedUpdate()
        {
            UpdateMovement();

            if (direction != Vector3.zero)
                RotateTowardsDirection();

            if (Input.GetKeyDown(pickUpItem))
                if (controller.CarryingItem())
                {
                    controller.Dropitem();
                }
                else
                {
                    controller.PickUpItem(GetClosestItem());
                }


        }

        private void UpdateMovement()
        {
            rigidBody.velocity = Vector3.zero;
            var movement = Vector3.zero;

            movement.x = Input.GetAxis("Horizontal");
            movement.z = Input.GetAxis("Vertical");

            if (movement == Vector3.zero)
                return;

            currentSpeed = controller.GetCurrentSpeed();

            direction = movement;

            movement *= currentSpeed * Time.fixedDeltaTime;
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

            Debug.Log("LookForItems");

            var colliders = Physics.OverlapBox(detectionTrigger.gameObject.transform.position,
                detectionTrigger.bounds.size / 2, Quaternion.identity, detectionLayers);

            /*Physics.OverlapBox(detectionTrigger.bounds.center, detectionTrigger.bounds.size,
                detectionTrigger.transform.forward, out hit, detectionTrigger.transform.rotation,
                detectionTrigger.bounds.size.magnitude, detectionTrigger.gameObject.layer)*/

            if (colliders.Length > 0)
            {
                Debug.Log("objects found");

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

    }
}



        /*private void OnTriggerEnter(Collider other)
        {
            collidingItems.Add(other.gameObject);
            
        }

        private void OnTriggerExit(Collider other)
        {
            if (collidingItems.Contains(other.gameObject))
            {
                collidingItems.Remove(other.gameObject);
            }
        }

    }
}*/

