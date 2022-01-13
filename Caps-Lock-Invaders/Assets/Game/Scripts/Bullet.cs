using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Bullet : MonoBehaviour
{
    public static Bullet Create(Vector3 spawnPosition, float bulletLifetime, bool isFriendlyBullet)
    {
        Transform bulletTransform = Instantiate(GameAssets.Instance.pfBullet, spawnPosition, Quaternion.identity);
        
        Bullet bullet = bulletTransform.GetComponent<Bullet>();
        bullet.SetIsFriendlyBullet(isFriendlyBullet);

        return bullet;
    }

    public event EventHandler OnBulletCreated;

    private float lifeTime = 2f;
    private float movementSpeed = 10f;
    private bool isFriendlyBullet;


    private void Start()
    {
        OnBulletCreated?.Invoke(this, EventArgs.Empty);
    }

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
        if (collision.CompareTag("Enemy"))
        {
            //TODO: ateþ bekleme süresini 0la
            Destroy(collision.attachedRigidbody.gameObject);
            Destroy(gameObject);
        }

        Obstacle obstacle = collision.attachedRigidbody.GetComponent<Obstacle>();
        if (obstacle != null)
        {
            obstacle.BulletHitMe();
            Destroy(gameObject);
        }
    }

    private void Move()
    {
        transform.position += Vector3.forward * movementSpeed * Time.deltaTime;
    }

    private void SetIsFriendlyBullet(bool isFriendlyBullet)
    {
        this.isFriendlyBullet = isFriendlyBullet;
    }

}
