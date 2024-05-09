using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;
    private PlayerAnimationController _playerAnimationController;
    private PlayerBulletController _playerBulletController;
    private Health _playerHealth;
    private bool _levelIsComplete;

    public void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
        _playerBulletController = GetComponentInChildren<PlayerBulletController>();
        _playerHealth = GetComponent<Health>();
    }

    public void Update()
    {
        if (_levelIsComplete) return; // No more ship controls once completed
        
        if (_playerHealth.GetHitPoints() <= 0)
        {
            HandleDeathInputs();
            return;
        }

        HandleMovementInputs();
        HandleWeaponInputs();
    }

    private void HandleDeathInputs()
    {
        if (Input.GetKey(KeyCode.R)) // Reset
        {
            _playerMovementController.Reset();
            _playerHealth.ResetHitPoints();
            _playerAnimationController.Reset();
        }
        else
        {
            _playerMovementController.ZeroInputs();
        }
    }

    private void HandleMovementInputs()
    {
        bool accelerating = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); 
        _playerMovementController.SetAccelerating(accelerating);

        float rotating = -Input.GetAxisRaw("Horizontal");
        _playerMovementController.SetRotationDirection(rotating);

        bool left = false;
        bool right = false;
        switch (rotating)
        {
            case > 0:
                right = true;
                break;
            case < 0:
                left = true;
                break;
        }
        _playerAnimationController.SetThrusters(left, accelerating, right);
    }

    private void HandleWeaponInputs()
    {
        if (Input.GetKey(KeyCode.Space)) _playerBulletController.TryShootingWeapon();
    }
    
    public void SetLevelCompleted(bool complete)
    {
        _levelIsComplete = complete;
    }
}