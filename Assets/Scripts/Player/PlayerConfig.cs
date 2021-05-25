using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerConfiguration", menuName = "Config/PlayerConfiguration")]
public class PlayerConfig : ScriptableObject
{
    public float defaultSpeed;
    public float sprintSpeed;
    public float boost;
    public float decelerationSpeed;
    public float stoppingSpeed;
    public float rotationSpeed;
}
