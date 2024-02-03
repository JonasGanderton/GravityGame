using UnityEngine;

public class BulletController : MonoBehaviour
{
    public GameObject bulletPrefab;
    
    public float fireForce = 0.05f;
    public float firingDelay = 0.1f;
    
    private Transform _firePoint;
    private float _nextFireTime;

    private void Awake()
    {
        _firePoint = GetComponent<Transform>();
        Debug.Log(_firePoint);
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (Time.time < _nextFireTime) return;
            Fire();
            _nextFireTime = Time.time + firingDelay;
        }
    }

    public void Fire()
    {
        GameObject bullet = Instantiate(bulletPrefab, _firePoint.position, _firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(0f, fireForce), ForceMode2D.Impulse);
    }
}
