using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;


public class PlayerController : MonoBehaviour, IDamageable
{
    [SerializeField] private PlayerSettings playerSettings;
    [SerializeField] private Transform sideMovementRoot, bulletSpawnPos, leftLimit, rightLimit;

    private Vector2 inputDrag, previousMousePosition;

    private float leftLimitX => leftLimit.localPosition.x;
    private float rightLimitX => rightLimit.localPosition.x;

    private float bulletLifeTimer = 0f;
    private bool canShoot => bulletLifeTimer <= 0f;

    private void Awake()
    {
        bulletSpawnPos = transform.Find("bulletSpawnPoint");
    }
    private void Update()
    {
        Timers();
        HandleInput();
        HandleSideMovement();
    }

    private void Timers()
    {
        if (bulletLifeTimer > 0f) bulletLifeTimer -= Time.deltaTime;
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            previousMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            var deltaMouse = (Vector2)Input.mousePosition - previousMousePosition;
            inputDrag = deltaMouse;
            previousMousePosition = Input.mousePosition;
        }
        else
        {
            inputDrag = Vector2.zero;
        }

        // can shoot check
        if (Input.GetKeyDown(KeyCode.Space) && canShoot)
        {
            Debug.Log(bulletSpawnPos.localPosition.x);
            Bullet.Create(bulletSpawnPos.localPosition, playerSettings.BulletLifetimeMax, true,
                () => { bulletLifeTimer = 0f; });
            bulletLifeTimer = playerSettings.BulletLifetimeMax;

        }
    }
    private void HandleSideMovement()
    {
        var localPos = sideMovementRoot.localPosition;
        localPos += Vector3.right * inputDrag.x * playerSettings.SideMovementSensitivity;

        bulletSpawnPos.localPosition = new Vector3(localPos.x, bulletSpawnPos.localPosition.y, bulletSpawnPos.localPosition.z);


        localPos.x = Mathf.Clamp(localPos.x, leftLimitX, rightLimitX);

        sideMovementRoot.localPosition = localPos;

        var moveDirection = Vector3.forward * 0.5f;
        moveDirection += sideMovementRoot.right * inputDrag.x * playerSettings.SideMovementSensitivity;

        moveDirection.Normalize();

        var targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);

        sideMovementRoot.rotation = Quaternion.Lerp(sideMovementRoot.rotation, targetRotation, Time.deltaTime * playerSettings.RotationSpeed);

    }
}