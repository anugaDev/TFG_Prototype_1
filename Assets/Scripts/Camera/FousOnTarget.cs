using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FousOnTarget : MonoBehaviour
{
    [SerializeField] private float smoothSpeed;

    [SerializeField] private Transform defaultTarget;
    private Transform actualTarget;

    private void Start()
    {
        ChangeTarget(defaultTarget);
    }

    private void FixedUpdate()
    {
        UpdatePosition();
    }
    private void UpdatePosition()
    {
        print("UpdateCamera");
        Vector3 desiredPosition = actualTarget.position;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        smoothedPosition.y = transform.position.y;
        transform.position = smoothedPosition;
        
    }
    public void ChangeTarget(Transform newTarget)
    {
        actualTarget = newTarget;
    }
}
