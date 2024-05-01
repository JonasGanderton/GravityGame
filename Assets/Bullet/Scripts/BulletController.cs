using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float firingDelay = 0.1f;
    [SerializeField] private int bulletsFired; // Included for quick viewing
    
    [SerializeField] private GameObject bulletPrefab;
    private GameObject[] _bulletPrefabs;

    private Transform _transform;

    private BulletBehaviour[] _bullets;
    private const int MaxBullets = 25;
    private int _nextBulletIndex = 0;
    
    private float _nextFireTime;

    private void Awake()
    {
        _transform = GetComponent<Transform>();

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

    public void Update()
    {
        if (!Input.GetKey(KeyCode.Space)) return; // Check if holding fire button - space

        if (Time.time < _nextFireTime) return; // Check if there has been a long enough gap

        if (_bullets[_nextBulletIndex].IsActive()) return; // Check if there are any available bullets

        _nextFireTime = Time.time + firingDelay;
        _bullets[_nextBulletIndex].Fire(_transform.position, _transform.rotation);
        SetNextBulletIndex();
        bulletsFired++;
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