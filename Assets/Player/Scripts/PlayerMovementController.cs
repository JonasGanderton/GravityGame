using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovementController : MonoBehaviour
{
    [SerializeField] private float mass = 2.011673f; // Reduced mass with larger collider for new sprite
    [SerializeField] private float accelerationRate = 16.09338f; // Reduced acceleration for reduced mass
    [SerializeField] private float rotationForce = 3;
    [SerializeField] private float angularDrag = 4.5f;
    [SerializeField] private float drag = 0.2f;
    
    private Rigidbody2D _rb;
    private Transform _tr;
    private float _currentAcceleration;
    private float _currentRotationForce;
    private Vector2 resetPosition;
    
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
        resetPosition = _rb.position;
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
        _rb.position = resetPosition;
        _rb.velocity = Vector2.zero;
        _rb.rotation = 0;
        _rb.angularVelocity = 0;
    }
}
