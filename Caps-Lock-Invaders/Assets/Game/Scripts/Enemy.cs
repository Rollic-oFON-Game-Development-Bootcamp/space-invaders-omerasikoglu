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

    //bullet, shoot
    private float bulletLifeTimer = 0f;
    private float bulletLifeTimerMax = 2f;
    bool canShoot => bulletLifeTimer <= 0f;

    //moving
    private float columnMoveTimer = 0f;
    private float columnMoveTimerMax => UnityEngine.Random.Range(4f, 5f);
    bool canMove => columnMoveTimer <= 0f;


    private void Update()
    {
        Timers();

        if (Input.GetKeyDown(KeyCode.C) && canShoot)
        {
            Debug.Log(bulletSpawnPoint.localPosition.x);
            Bullet.Create(bulletSpawnPoint.position, bulletLifeTimerMax, false, () => { Debug.Log("aksiyoonEnemyy"); });
            bulletLifeTimer = bulletLifeTimerMax;

        }
        if (canMove)
        {
            transform.position = new Vector3(transform.position.x + -1f, transform.position.y, transform.position.z);
            bulletSpawnPoint.position = new Vector3(transform.position.x, bulletSpawnPoint.position.y, bulletSpawnPoint.position.z);
            columnMoveTimer = columnMoveTimerMax;
        }
    }

    private void Timers()
    {
        if (bulletLifeTimer > 0f) bulletLifeTimer -= Time.deltaTime;
        if (columnMoveTimer > 0f) columnMoveTimer -= Time.deltaTime;
    }
}
