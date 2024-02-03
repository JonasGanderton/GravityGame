using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public float fireForce = 0.05f;

    public void Fire(Transform firePoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, fireForce), ForceMode2D.Impulse);
    }

    public void OnCollisionEnter2D(Collision2D other)
    { 
        Destroy(gameObject);
    }
}
