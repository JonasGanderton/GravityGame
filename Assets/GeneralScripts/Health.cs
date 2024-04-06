using UnityEngine;

public class Health : MonoBehaviour
{
    
    [SerializeField] private int maxHitPoints = 50;
    [SerializeField] private int currentHitPoints;

    private void Awake()
    {
        currentHitPoints = maxHitPoints;
    }

    public void DoDamage(int damage)
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