using UnityEngine;

public class EnemyBulletController : MonoBehaviour
{
    [SerializeField] private float firingDelay = 1f;
    
    [SerializeField] private GameObject bulletPrefab;
    private GameObject[] _bulletPrefabs;
    
    private BulletBehaviour[] _bullets;
    private const int MaxBullets = 2;
    private int _nextBulletIndex = 0;
    
    private float _nextFireTime;

    private AimDirectController _aimDirectController; // Could use inheritance for different AimControllers (e.g. AimAheadController)
    private float _bulletDamage = 5f;

    private void Awake()
    {
        _bullets = new BulletBehaviour[MaxBullets];
        _bulletPrefabs = new GameObject[MaxBullets];

        for (int i = 0; i < MaxBullets; i++)
        {
            _bulletPrefabs[i] = Instantiate(bulletPrefab);
            _bullets[i] = _bulletPrefabs[i].GetComponent<BulletBehaviour>();
            _bullets[i].SetDamage(_bulletDamage);
        }
        
        _nextFireTime = Time.time + firingDelay;

        _aimDirectController = this.GetComponent<AimDirectController>();

    }

    private void SetNextBulletIndex()
    {
        _nextBulletIndex++;
        if (_nextBulletIndex == MaxBullets)
        {
            _nextBulletIndex = 0;
        }
    }

    private void FixedUpdate()
    {
        if (!_aimDirectController.TargetVisible()) return; // Can't see target
        if (Time.time < _nextFireTime) return; // Check if there has been a long enough gap
        if (_bullets[_nextBulletIndex].IsActive()) return; // Check if there are any available bullets
        _nextFireTime = Time.time + firingDelay;
        _bullets[_nextBulletIndex].Fire(transform.position, transform.rotation);
        SetNextBulletIndex();
    }
}