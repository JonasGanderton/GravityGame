using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float mass = 25;
    [SerializeField] private float accelerationRate = 200;
    [SerializeField] private float rotationForce = 3;
    [SerializeField] private float angularDrag = 4.5f;
    [SerializeField] private float drag = 0.2f;
    [SerializeField] private float crashingDamageMultiplier = 10f;
    [SerializeField] private float damageThreshold = 0.5f;
    
    private Rigidbody2D _rb;
    private Transform _tr;
    private float _currentAcceleration;
    private float _currentRotationForce;
    
    public void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _tr = GetComponent<Transform>();
        _rb.mass = mass;
        _rb.drag = drag;
        _rb.angularDrag = angularDrag;
        _rb.gravityScale = 0.3f;
        _currentAcceleration = 0;
        _currentRotationForce = 0;
    }

    public void SetAccelerating(bool accelerate)
    {
        if (accelerate)
        {
            _currentAcceleration = accelerationRate;
        }
        else
        {
            _currentAcceleration = 0;
        }
    }

    private void Accelerate()
    {
        _rb.AddForce(_tr.up * _currentAcceleration);
    }

    public void SetRotationDirection(float direction)
    {
        // -1 : anticlockwise, 0 : no rotation, 1 : clockwise
        _currentRotationForce = direction * rotationForce;
    }

    private void Rotate()
    {
        _rb.AddTorque(_currentRotationForce);
    }

    private void FixedUpdate()
    {
        Accelerate();
        Rotate();
    }

    public void Reset()
    {
        //transform.position = Vector3.zero;
        _rb.position = Vector2.zero;
        _rb.velocity = Vector2.zero;
        _rb.rotation = 0;
        _rb.angularVelocity = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            // Add invincibility frames?
            float damage = _rb.velocity.sqrMagnitude * crashingDamageMultiplier;
            if (damage > damageThreshold)
            {
                this.gameObject.SendMessage("DoDamage", damage);
            }
        }
    }
}
