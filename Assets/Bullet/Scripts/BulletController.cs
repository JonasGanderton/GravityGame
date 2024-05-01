using UnityEngine;

public abstract class BulletController : MonoBehaviour
{
    [SerializeField] private float firingDelay = 0.1f;
    
    [SerializeField] private GameObject bulletPrefab;
    private GameObject[] _bulletPrefabs;

    private BulletBehaviour[] _bullets;
    private const int MaxBullets = 25;
    private int _nextBulletIndex = 0;

    private float _nextFireTime;

    private void Awake()
    {
        _bullets = new BulletBehaviour[MaxBullets];
        _bulletPrefabs = new GameObject[MaxBullets];

        for (int i = 0; i < MaxBullets; i++)
        {
            _bulletPrefabs[i] = Instantiate(bulletPrefab);
            _bulletPrefabs[i].GetComponent<Health>().SetMaxHealth(1);
            _bullets[i] = _bulletPrefabs[i].GetComponent<BulletBehaviour>();
        }
    }

    private void SetNextBulletIndex()
    {
        _nextBulletIndex++;
        if (_nextBulletIndex == MaxBullets)
        {
            _nextBulletIndex = 0;
        }
    }

    private bool CanShoot()
    {
        if (Time.time < _nextFireTime) return false; // Check if there has been a long enough gap
        
        return !_bullets[_nextBulletIndex].IsActive(); // Check if there are any available bullets
        
        // Could loop through to next bullet(s) in case a bullet takes longer to return?
        // Potential for hidden bugs slowly decreasing number of bullets available
    }

    protected void Shoot()
    {
        if (!CanShoot()) return;

        _nextFireTime = Time.time + firingDelay;
        _bullets[_nextBulletIndex].Fire(transform.position, transform.rotation);
        SetNextBulletIndex();
    }

    public void BoostDamageAll(float multiplier)
    {
        foreach (BulletBehaviour bullet in _bullets)
        {
            bullet.BoostDamage(multiplier);
        }
    }

    public void DecreaseReloadTime(float multiplier)
    {
        firingDelay *= multiplier;
    }

    public void AddBounceAll(int numBounces)
    {
        foreach (BulletBehaviour bullet in _bullets)
        {
            bullet.AddBounce(numBounces);
        }
    }
}