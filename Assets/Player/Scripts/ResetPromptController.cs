using UnityEngine;

public class ResetPromptController : MonoBehaviour
{
    private SpriteRenderer[] _sprites;
    private Transform _playerTransform;
    private bool _isActive;

    private void Awake()
    {
        _playerTransform = GetComponentInParent<Transform>();
        _sprites = GetComponentsInChildren<SpriteRenderer>();
        SetActive(false);
    }

    private void LateUpdate()
    {
        if (!_isActive) return;
        
        // Always be directly above player
        transform.rotation = _playerTransform.rotation;
        transform.Rotate(Vector3.forward, -_playerTransform.rotation.eulerAngles[2]);
    }

    public void SetActive(bool active)
    {
        _isActive = active;
        foreach (SpriteRenderer sprite in _sprites)
        {
            sprite.enabled = active;
        }
    }
}