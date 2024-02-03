using UnityEngine;

public class Bullet : MonoBehaviour
{
    public void OnCollisionEnter2D()
    {
        // Differentiate between environment and enemy collisions here
        Destroy(gameObject);
    }
}