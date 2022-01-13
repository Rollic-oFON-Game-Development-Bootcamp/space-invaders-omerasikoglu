using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private float decreaseScale = 0.15f;
    public void BulletHitMe()
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y - decreaseScale, transform.localScale.z);
    }
}
