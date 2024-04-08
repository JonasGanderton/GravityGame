using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private float crashingDamageMultiplier = 3f;
    [SerializeField] private float damageThreshold = 5f;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            EnvironmentCollision(collision);
        }
        else if (collision.gameObject.CompareTag("PickUp"))
        {
            PickUpCollision(collision);
        }
    }

    private void EnvironmentCollision(Collision2D collision)
    {
        // Add invincibility frames?
            
        // Reduces damage linearly between a direct crash having full damage, to a glance (parallel to surface) doing 20%
        float reduceOnAngledCrash = 1 - (Vector2.Angle(collision.contacts[0].normal, collision.relativeVelocity) * Mathf.Deg2Rad * 1.6f / Mathf.PI);
        float damage = reduceOnAngledCrash * collision.relativeVelocity.magnitude * crashingDamageMultiplier;
        if (damage > damageThreshold)
        {
            this.gameObject.SendMessage("DoDamage", damage);
        }
    }

    private void PickUpCollision(Collision2D collision)
    {
        // Get effect of PickUp
        Destroy(collision.gameObject);
    }
}