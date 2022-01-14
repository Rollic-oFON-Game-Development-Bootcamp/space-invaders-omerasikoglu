using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/// <summary>
/// column => shoot
/// row => movement
/// </summary>
public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] private Transform bulletSpawnPoint;

    //bullet lifetime
    private float bulletLifeTimer = 0f;
    private float bulletLifeTimerMax = 2f;

    //shoot
    private float shootTimer = 0f;
    private float shootTimerMax;
    bool canShoot => shootTimer <= 0f;

    //moving ai
    private float columnMoveTimer = 0f;
    private float columnMoveTimerMax;
    private float maxX = 6f, minX = -5f, transitionSpeed = 2f;
    private bool canMove => columnMoveTimer <= 0f;
    

    private void Awake()
    {
        columnMoveTimerMax = UnityEngine.Random.Range(4f, 5f);
        shootTimerMax = UnityEngine.Random.Range(1f, 5f);
    }
    private void Update()
    {
        Timers();
        HandleShooting();
        HandleMovement();
    }

    private void HandleShooting()
    {
        ManuelShooting();
        AutomaticShooting();
    }

    private void AutomaticShooting()
    {
        if (canShoot)
        {
            if (UnityEngine.Random.Range(0f, 10f) < 1f)
            {
                Shoot();
            }
            else
            {
                shootTimer = shootTimerMax;
            }
        }
    }

    private void ManuelShooting()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        Bullet.Create(bulletSpawnPoint.position, bulletLifeTimerMax, false,
                  () => { /*Debug.Log("aksiyoonEnemyy");*/ });
        shootTimer = shootTimerMax;
        bulletLifeTimer = bulletLifeTimerMax;
    }

    private void HandleMovement()
    {
        if (canMove)
        {
            if (transform.position.x <= minX)
            {
                DOTween.Kill(transform);
                transform.position = new Vector3(maxX, transform.position.y, transform.position.z);
                transform.DOMoveZ(transform.position.z - 5f, 5f);
                columnMoveTimerMax -= 2f;
                columnMoveTimerMax = Mathf.Clamp(columnMoveTimerMax, .8f, 6f);
                transitionSpeed -= 1f;
                transitionSpeed = Mathf.Clamp(transitionSpeed, .8f, 2f);
            }
            transform.DOMoveX(transform.position.x - 2f, transitionSpeed).SetEase(Ease.InOutSine);
            bulletSpawnPoint.position = new Vector3(transform.position.x, bulletSpawnPoint.position.y, bulletSpawnPoint.position.z);
            columnMoveTimer = columnMoveTimerMax;

        }
    }

    private void Timers()
    {
        if (bulletLifeTimer > 0f) bulletLifeTimer -= Time.deltaTime;
        if (columnMoveTimer > 0f) columnMoveTimer -= Time.deltaTime;
        if (shootTimer > 0f) shootTimer -= Time.deltaTime;
    }
}
