using UnityEngine;

public class PlayerBulletController : BulletController
{ 
    private bool CanShoot()
    {
        return Input.GetKey(KeyCode.Space); // Check if holding fire button - space
        //return base.CanShoot();
    }

    public void Update()
    {
        if (CanShoot()) Shoot();
    }
}