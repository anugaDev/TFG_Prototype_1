using System.Collections;
using System.Collections.Generic;
using Reliquary.Player;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum playerStates { QUIET, WALKING, RUNNING }
    [HideInInspector] public playerStates currentPlayerState;

    [SerializeField] private KeyCode upKey = KeyCode.W;
    [SerializeField] private KeyCode downKey = KeyCode.S;
    [SerializeField] private KeyCode leftKey = KeyCode.A;
    [SerializeField] private KeyCode rightKey = KeyCode.D;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [SerializeField] private float defaultSpeed;
    [SerializeField] private float sprintSpeed;
    [SerializeField] private float boost;
    [SerializeField] private float decelerationSpeed;
    [SerializeField] private float stoppingSpeed;
    [SerializeField] private float rotationSpeed;

    private Vector3 direction;
    private float currentSpeed;
    private Rigidbody playerRb;

    public OnPlayerMovedCommand onPlayerMovedCommand;
    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        currentPlayerState = playerStates.QUIET;
        currentSpeed = 0;
    }
    private void FixedUpdate()
    {
        UpdateMovement();

        if(direction!= Vector3.zero)
            RotateTowardsDirection();
    }
    private void UpdateMovement()
    {
        playerRb.velocity = Vector3.zero;
        var movement = Vector3.zero;

        if (Input.GetKey(upKey))
        {
            movement.z += 1;
        }
        if (Input.GetKey(downKey))
        {
            movement.z -= 1;
        }
        if (Input.GetKey(rightKey))
        {
            movement.x += 1;
        }
        if (Input.GetKey(leftKey))
        {
            movement.x -= 1;
        }
        UpdateSpeed();
        //if(movement!=Vector3.zero || (movement==Vector3.zero&&actualSpeed==0))
        movement = Mathf.Abs(movement.x) == 1.0f && Mathf.Abs(movement.z) == 1.0f ? new Vector3 (0.5f * Mathf.Sign(movement.x), 0, 0.5f * Mathf.Sign(movement.z)) : movement;

        direction = movement;
        movement *= currentSpeed * Time.fixedDeltaTime;
        playerRb.velocity = movement;

        onPlayerMovedCommand.Execute(transform.position);


    }
    private void RotateTowardsDirection()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.fixedDeltaTime * rotationSpeed);
    }
    private void UpdateSpeed()
    {
        if(Input.GetKey(sprintKey)&&direction!=Vector3.zero)
        {
            currentPlayerState = playerStates.RUNNING;
            if (/*Input.GetKeyDown(sprintKey) ||*/ currentSpeed <= defaultSpeed)
                currentSpeed = sprintSpeed + boost;
            else
                currentSpeed = currentSpeed > sprintSpeed ? currentSpeed - decelerationSpeed : sprintSpeed;
        }
        else if (direction!=Vector3.zero)
        {
            currentPlayerState = playerStates.WALKING;
            currentSpeed = currentSpeed > defaultSpeed ? currentSpeed - decelerationSpeed : defaultSpeed;
        }
        else
        {
            currentPlayerState = playerStates.QUIET;
            currentSpeed = currentSpeed > 0 ? currentSpeed - stoppingSpeed : 0;
        }
    }


}
