using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] private float maxHitPoints = 50;
    [SerializeField] private float currentHitPoints;

    private void Awake()
    {
        currentHitPoints = maxHitPoints;
    }

    public void SetMaxHealth(float newMaxHitPoints)
    {
        maxHitPoints = newMaxHitPoints;
        currentHitPoints = maxHitPoints; // If increase max without regain hp, remove this line and change awake() to start()
    }

    public void DoDamage(float damage)
    {
        currentHitPoints -= damage;
        if (this.CompareTag("Player"))
        {
            this.gameObject.SendMessage("DamageSprite", currentHitPoints/maxHitPoints);
        }
        if (currentHitPoints <= 0)
        {
            NoHealth();
        }
    }

    private void NoHealth()
    {
        if (this.CompareTag("Player"))
        {
            this.gameObject.SendMessage("DeathAnimation");
        }
        else if (this.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
        else if (this.CompareTag("Projectile"))
        {
            this.gameObject.SendMessage("SetInactive");
        }
    }

    public float GetHitPoints()
    {
        return currentHitPoints;
    }

    public void ResetHitPoints()
    {
        currentHitPoints = maxHitPoints;
    }
}