using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static Bullet Create(Vector3 spawnPosition, float bulletLifetime, bool isFriendlyBullet, Action ResetPlayerShootTimer)
    {
        Transform bulletTransform = isFriendlyBullet ? Instantiate(GameAssets.Instance.pfBullet, spawnPosition, Quaternion.identity) :
           Instantiate(GameAssets.Instance.pfBullet, spawnPosition, Quaternion.identity);

        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetIsFriendlyBullet(isFriendlyBullet);
        bullet.SetResetPlayerShootTimer(ResetPlayerShootTimer);

        return bullet;
    }

    private Action ResetPlayerShootTimer;

    private float lifeTime = 2f;
    private float movementSpeed = 10f;
    private bool isFriendlyBullet;

    private void Update()
    {
        Move();
        Timers();
    }

    private void Timers()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0f)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        //while player shooting
        if (isFriendlyBullet && collision.CompareTag("Enemy"))
        {
            ResetPlayerShootTimer();
            Destroy(collision.attachedRigidbody.gameObject);
            Destroy(gameObject);
        }//two bullet hit
        if (collision.CompareTag("Bullet"))
        {
            ResetPlayerShootTimer();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        //while enemy shooting
        if (!isFriendlyBullet && collision.CompareTag("Player"))
        {
            Destroy(collision.attachedRigidbody.gameObject);
            Destroy(gameObject);
        }
        //garibim obstacle
        if (collision.CompareTag("Obstacle"))
        {
            if (isFriendlyBullet)
            {
                ResetPlayerShootTimer(); 
            }
            collision.attachedRigidbody.GetComponent<Obstacle>().BulletHitMe();
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += isFriendlyBullet ? Vector3.forward * movementSpeed * Time.deltaTime : 
            Vector3.back * movementSpeed * Time.deltaTime;
    }

    private void SetIsFriendlyBullet(bool isFriendlyBullet)
    {
        this.isFriendlyBullet = isFriendlyBullet;
    }
    private void SetResetPlayerShootTimer(Action Reset)
    {
        this.ResetPlayerShootTimer = Reset;
    }

}
