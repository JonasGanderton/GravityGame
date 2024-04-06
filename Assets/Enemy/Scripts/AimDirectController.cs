using UnityEngine;

// Aims directly at target
public class AimDirectController : MonoBehaviour
{
    private GameObject _target;
    private void Awake()
    {
        _target = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        transform.LookAt(_target.transform);
        transform.Rotate(Vector3.right * 90);
        transform.Rotate(Vector3.down * 90);
    }
}