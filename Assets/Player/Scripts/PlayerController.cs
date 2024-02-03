using UnityEngine;

public class PlayerController : MonoBehaviour
{
	private Rigidbody2D _rigidbody2D;
	
	private void Awake()
	{
		_rigidbody2D = GetComponent<Rigidbody2D>();
	}
	
	public void Accelerate(float acceleration)
	{
		_rigidbody2D.AddRelativeForce(new Vector2(0f, acceleration));
	}

	public void Rotate(float rotation)
	{
		// Apply smoothing to rotation?
		_rigidbody2D.transform.Rotate(0, 0, rotation);
	}
}