using System;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    /*
     * Get SpriteRenderers for Thrusters + weapon flash
     * Only show thrusters when firing (loop different frames or add stretch/squish/shake)
     * Show weapon flash and go through 3-5 frames (check Kenney's pack)
     * 
     */
    
    private SpriteRenderer leftThruster;
    private SpriteRenderer middleThruster;
    private SpriteRenderer rightThruster;
    private SpriteRenderer weaponFlash;

    private float FlashActiveUntil;
    
    private void Awake()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        weaponFlash = sprites[1];
        leftThruster = sprites[2];
        middleThruster = sprites[3];
        rightThruster = sprites[4];
        
        leftThruster.enabled = false;
        middleThruster.enabled = false;
        rightThruster.enabled = false;
        weaponFlash.enabled = false;

        FlashActiveUntil = 0;
    }

    public void SetThrusters(bool left, bool middle, bool right)
    {
        leftThruster.enabled = left;
        middleThruster.enabled = middle;
        rightThruster.enabled = right;
    }

    public void DoWeaponFlash()
    {
        weaponFlash.enabled = true;
        FlashActiveUntil = Time.time + 0.05f;
    }

    private void FixedUpdate()
    {
        if (weaponFlash.enabled && Time.time > FlashActiveUntil) weaponFlash.enabled = false;
    }
    /*
    private void Update()
    {
        weaponFlash.enabled
    }*/
}