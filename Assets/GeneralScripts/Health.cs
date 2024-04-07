using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] private float maxHitPoints = 50;
    [SerializeField] private float currentHitPoints;

    private void Awake()
    {
        currentHitPoints = maxHitPoints;
    }

    public void DoDamage(float damage)
    {
        currentHitPoints -= damage;
        if (currentHitPoints <= 0)
        {
            NoHealth();
        }
    }

    private void NoHealth()
    {
        if (this.CompareTag("Player"))
        {
            this.gameObject.SendMessage("Reset");
            currentHitPoints = maxHitPoints;
        }
        else if (this.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }
        
}