using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerController controller;
    public float spinSpeed = 180f;
    public float accelerationRate = 450f; 
    // Acceleration for use with Gravity = 0.3
    
    private float _rotate;
    private float _accelerate;
    
    void Update()
    {
        // TODO: Add input manager to replace "space" with "accelerate"
        // https://docs.unity3d.com/Manual/class-InputManager.html
        // accelerate = Input.GetButton("space");
        // Could use if a or b, then include secondary mappings (i.e. WASD/arrow keys)
        
        // Check acceleration input
        if (Input.GetKey(KeyCode.Space) || Input.GetAxisRaw("Vertical") > 0)
        {
            _accelerate = accelerationRate;
        }
        else
        {
            _accelerate = 0f;
        }
        
        // Check rotation input
        _rotate = -Input.GetAxisRaw("Horizontal") * spinSpeed;
    }

    private void FixedUpdate()
    {
        if (_accelerate != 0) controller.Accelerate(_accelerate * Time.fixedDeltaTime);
        if (_rotate != 0) controller.Rotate(_rotate * Time.fixedDeltaTime);
    }
}
