using UnityEngine;

// Aims directly at target
public class AimDirectController : MonoBehaviour
{
    [SerializeField] private float viewRange = 15;
    private GameObject _target;
    private bool _targetVisible;
    private void Awake()
    {
        _target = GameObject.FindWithTag("Player");
    }

    private void FixedUpdate()
    {
        Vector3 targetDirection = _target.transform.position - transform.position;
        RaycastHit2D hit2D = Physics2D.Raycast(transform.position, targetDirection, viewRange, LayerMask.GetMask("Player", "Environment"));

        if (!hit2D)
        {
            _targetVisible = false;
        }
        else if (hit2D.transform == _target.transform)
        {
            transform.LookAt(_target.transform);
            transform.Rotate(Vector3.right * 90);
            transform.Rotate(Vector3.down * 90);
            _targetVisible = true;
        }
        else
        {
            _targetVisible = false;
        }
    }

    public bool TargetVisible()
    {
        return _targetVisible;
    }
}