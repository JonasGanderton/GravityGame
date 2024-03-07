using UnityEngine;

public class PlayerInputController : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;

    public void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    public void Update()
    {
        // Use InputManager.GetButton("action") to allow remapping
        
        // Allow use of W or UpArrow
        _playerMovementController.SetAccelerating(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        // Allow use of A/D or LeftArrow/RightArrow
        _playerMovementController.SetRotationDirection(-Input.GetAxisRaw("Horizontal"));
    }
}