using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private float firingDelay = 0.06f;
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
        if (!Input.GetKey(KeyCode.Space)) return;

        if (Time.time < _nextFireTime) return;

        if (_bullets[_nextBulletIndex].IsActive()) return;

        _nextFireTime = Time.time + firingDelay;
        _bullets[_nextBulletIndex].Fire(_transform.position, _transform.rotation);
        SetNextBulletIndex();
        bulletsFired++;
    }
}