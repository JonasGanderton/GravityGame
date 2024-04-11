using System;
using UnityEngine;

public class PickUpEffect : MonoBehaviour
{
    private enum Effect
    {
        BoostDamage,
        DecreaseReloadTime,
        AddBounce
    }
    
    [SerializeField] private Effect effect = Effect.BoostDamage;
    [SerializeField] private float effectDuration = 10f;
    [SerializeField] private float damageBoost = 5f;
    [SerializeField] private float reloadTimeDecrease = 0.5f;
    [SerializeField] private int extraBounces = 1;
    
    private SpriteRenderer _spriteRenderer;
    private CircleCollider2D _circleCollider2D;
    private BulletController _bulletController;
    private bool _effectIsActive;
    private float _deactivateTime;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _circleCollider2D = GetComponent<CircleCollider2D>();
        _bulletController = GameObject.FindWithTag("PlayerWeapon").GetComponent<BulletController>();
    }

    private void SetIsReady(bool active)
    {
        _spriteRenderer.enabled = active;
        _circleCollider2D.enabled = active;
    }
    
    public void Activate()
    {
        SetIsReady(false);
        
        if (effect == Effect.BoostDamage)
        {
            _bulletController.BoostDamageAll(damageBoost);
        }
        else if (effect == Effect.DecreaseReloadTime)
        {
            _bulletController.DecreaseReloadTime(reloadTimeDecrease);
        }
        else if (effect == Effect.AddBounce)
        {
            _bulletController.AddBounceAll(extraBounces);
        }

        _effectIsActive = true;
        _deactivateTime = Time.time + effectDuration;
    }

    private void Deactivate()
    {
        if (effect == Effect.BoostDamage)
        {
            _bulletController.BoostDamageAll(1/damageBoost);
        }
        else if (effect == Effect.DecreaseReloadTime)
        {
            _bulletController.DecreaseReloadTime(1/reloadTimeDecrease);
        } 
        else if (effect == Effect.AddBounce)
        {
            _bulletController.AddBounceAll(-extraBounces);
        }

        _effectIsActive = false;
    }

    private void FixedUpdate()
    {
        if (!_effectIsActive) return; // Not activated yet
        if (Time.time < _deactivateTime) return; // Still active

        Deactivate();
        SetIsReady(true);
    }
}
