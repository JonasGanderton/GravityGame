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

    [SerializeField] private float initialSpeed = 10f;
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
            SetInactive();
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
    }
}