using UnityEngine;

public class PlayerBulletController : BulletController
{
    private new void Awake()
    {
        MaxBullets = 50;
        base.Awake();
    }
    public void TryShootingWeapon()
    {
        Shoot();
    }
}