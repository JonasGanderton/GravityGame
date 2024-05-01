using UnityEngine;

public class EnemyBulletController : BulletController 
{
    private AimDirectController _aimDirectController; // Could use inheritance for different AimControllers (e.g. AimAheadController)

    private new void Awake()
    {
        FiringDelay = 1f;
        MaxBullets = 2;
        BulletDamage = 10f;
        _aimDirectController = this.GetComponent<AimDirectController>();
        base.Awake();
    }
    
    private bool CanShoot()
    {
        return _aimDirectController.TargetVisible(); // If target is visible
    }

    private void FixedUpdate()
    {
        if (CanShoot()) Shoot();
    }
}