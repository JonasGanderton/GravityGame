using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour
{
    [SerializeField] private float crashingDamageMultiplier = 3f;
    [SerializeField] private float damageThreshold = 5f;

    private GameObject _levelCompleteHandler;
    
    private float pickUpDelay = 0.1f;
    private float nextPickUpTime;
    private bool canCompleteLevel;
    
    private void Awake()
    {
        _levelCompleteHandler = GameObject.FindWithTag("LevelCompleteMenu");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Environment"))
        {
            EnvironmentCollision(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PickUp"))
        {
            PickUpItem(other);
        }
        else if (other.gameObject.CompareTag("Goal"))
        {
            canCompleteLevel = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Goal"))
        {
            canCompleteLevel = false;
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
            canCompleteLevel = false;
            this.gameObject.SendMessage("DoDamage", damage);
        }
        else if (canCompleteLevel)
        {
            Debug.Log("TODO: Level completed!");
            _levelCompleteHandler.SendMessage("LevelComplete");
            // Send message to LevelController - level complete.
        }
    }

    private void PickUpItem(Collider2D other)
    {
        // Prevent double pick up
        if (Time.time >= nextPickUpTime)
        {
            other.gameObject.SendMessage("Activate");
            nextPickUpTime = Time.time + pickUpDelay;
        }
    }
}