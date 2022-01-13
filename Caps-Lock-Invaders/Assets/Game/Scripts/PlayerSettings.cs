using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Settings/Player")]
public class PlayerSettings : ScriptableObject
{

    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float bulletLifetimeMax = 2f;
    [SerializeField] private float sideMovementSensitivity = 1f;
    [SerializeField] private float rotationSpeed = 1f;
    public float MovementSpeed => playerMovementSpeed;
    public float BulletLifetimeMax => bulletLifetimeMax;
    public float SideMovementSensitivity => sideMovementSensitivity;
    public float RotationSpeed => rotationSpeed;
}

