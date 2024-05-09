using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class BulletBehaviour : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _boxCollider2D;
    private Rigidbody2D _rigidbody2D;
    private Transform _transform;
    private bool _isActive;
    private int _maxBounces = 0;
    private int _bouncesRemaining;

    [SerializeField] private float initialSpeed = 15f;
    private float _damage = 10f;
    
    public void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _transform = GetComponent<Transform>();
        SetInactive();
    }

    private void SetInactive()
    {
        _spriteRenderer.enabled = false;
        _boxCollider2D.enabled = false;
        _rigidbody2D.Sleep();
        _isActive = false;
    }

    private void SetActive()
    {
        _spriteRenderer.enabled = true;
        _boxCollider2D.enabled = true;
        _rigidbody2D.WakeUp();
        _isActive = true;
        _bouncesRemaining = _maxBounces;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision matrix is set such that:
        // Player bullets hit enemies, enemy projectiles, and the environment
        // Enemy bullets only hit the player, player projectiles, and the environment
        string collisionTag = collision.gameObject.tag;
        if (collisionTag is "Projectile")
        {
            // Set bullet damage above, change by getting different ship/weapon/power-up
            collision.gameObject.SendMessage("DoDamage", _damage); 
        }
        else if (collisionTag is "Enemy" or "Player")
        {
            // Set bullet damage above, change by getting different ship/weapon/power-up
            collision.gameObject.SendMessage("DoDamage", _damage);
            SetInactive();
        }
        else if (collisionTag is "Environment")
        {
            if (_bouncesRemaining <= 0)
            {
                SetInactive();
            }
            else
            {
                _bouncesRemaining--;
                transform.LookAt(new Vector2(transform.position.x, transform.position.y) + _rigidbody2D.velocity);
                
                // LookAt rotates the sprite, so we rotate it back (x += 90 degrees)
                transform.Rotate(Vector3.right * 90);
                
                // If the collision is at (or very near to) 90 degrees to a horizontal surface
                // - We don't need to rotate around they axis
                // - The angle is typically < 0.001 degrees from 0 or 180
                // However, most of the time we don't have such a collision
                // and need to rotate downwards (y -= 90 degrees)
                if (transform.rotation.eulerAngles.y is not (< 0.1f or (> 179.9f and < 180.1f) or > 359.9f))
                {
                    transform.Rotate(Vector3.down * 90);
                }
                
                // If more problems later, check with near-parallel collisions
                // Should be sorted with check above, but not 100% sure.
            }
        }
    }

    public void Fire(Vector3 position, Quaternion rotation)
    {
        SetActive();
        _transform.SetPositionAndRotation(position, rotation);
        _rigidbody2D.velocity = _transform.up * initialSpeed;
    }

    public bool IsActive()
    {
        return _isActive;
    }

    public void SetDamage(float newDamage)
    {
        _damage = newDamage;
    }

    public void BoostDamage(float multiplier)
    {
        _damage *= multiplier;
        
        Vector3 newScale = Vector3.one;
        // Increase size proportional to boost
        // Otherwise reset to original size
        if (multiplier > 1) newScale *= (1 + multiplier/10);
        transform.localScale = newScale;
    }

    public void AddBounce(int numBounces)
    {
        _maxBounces += numBounces;
    }
}