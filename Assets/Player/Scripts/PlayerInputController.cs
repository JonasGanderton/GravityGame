using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;
    private PlayerAnimationController _playerAnimationController;

    public void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerAnimationController = GetComponent<PlayerAnimationController>();
    }

    public void Update()
    {
        // Use InputManager.GetButton("action") to allow remapping
        
        bool accelerating = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow); 
        _playerMovementController.SetAccelerating(accelerating);
        //_playerAnimationController.SetThrusters(accelerating, accelerating);

        float rotating = -Input.GetAxisRaw("Horizontal");
        _playerMovementController.SetRotationDirection(rotating);
        switch (rotating)
        {
            case 0:
                _playerAnimationController.SetThrusters(accelerating, accelerating);
                break;
            case > 0:
                _playerAnimationController.SetThrusters(true, false);
                break;
            case < 0:
                _playerAnimationController.SetThrusters(false, true);
                break;
        }

        // Allow use of W or UpArrow
        //_playerMovementController.SetAccelerating(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        // Allow use of A/D or LeftArrow/RightArrow
        //_playerMovementController.SetRotationDirection(-Input.GetAxisRaw("Horizontal"));
    }
}