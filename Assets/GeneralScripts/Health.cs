using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int hitPoints = 50;
    
    public void DoDamage(int damage)
    {
        hitPoints -= damage;
        if (hitPoints <= 0)
        {
            Destroy(this.gameObject);
        }
    }
        
}