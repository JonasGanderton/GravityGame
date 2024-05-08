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
    private SpriteRenderer damageLow;
    private SpriteRenderer damageMedium;
    private SpriteRenderer damageHigh;
    private TrailRenderer trail;

    private float flashActiveUntil;
    private float trailInactiveUntil;

    private bool isDead;
    
    private void Awake()
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        weaponFlash = sprites[1];
        leftThruster = sprites[2];
        middleThruster = sprites[3];
        rightThruster = sprites[4];
        damageLow = sprites[5];
        damageMedium = sprites[6];
        damageHigh = sprites[7];
        trail = GetComponentInChildren<TrailRenderer>();
        DisablePlayerResponsiveSprites();

        flashActiveUntil = 0;
        isDead = false;
    }

    private void DisablePlayerResponsiveSprites()
    {
        leftThruster.enabled = false;
        middleThruster.enabled = false;
        rightThruster.enabled = false;
        weaponFlash.enabled = false;
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
        flashActiveUntil = Time.time + 0.05f;
    }

    private void FixedUpdate()
    {
        if (isDead) return;
        if (weaponFlash.enabled && Time.time > flashActiveUntil) weaponFlash.enabled = false;
        if (!trail.emitting && Time.time > trailInactiveUntil) trail.emitting = true;
    }

    public void DeathAnimation()
    {
        if (isDead) return; // Prevent calling this multiple times
        
        trail.emitting = false;
        isDead = true;
        DisablePlayerResponsiveSprites();
        
        // TODO death animation
    }

    public void Reset()
    {
        trailInactiveUntil = Time.time + 0.1f;
        isDead = false;
        damageLow.enabled = false;
        damageMedium.enabled = false;
        damageHigh.enabled = false;
    }

    public void DamageSprite(float hitPointsProportion)
    {
        if (hitPointsProportion <= 0.25)
        {
            damageLow.enabled = false;
            damageMedium.enabled = false;
            damageHigh.enabled = true;
        }
        else if (hitPointsProportion <= 0.5)
        {
            damageLow.enabled = false;
            damageMedium.enabled = true;
        }
        else if (hitPointsProportion <= 0.75)
        {
            damageLow.enabled = true;
        }
    }
}