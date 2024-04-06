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
    }
}