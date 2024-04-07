using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject[] patrolRoutes;
    private GameObject[] enemies;
    private int _patrollingEnemies;
    

    private void Awake()
    {
        _patrollingEnemies = patrolRoutes.Length;
        Debug.Log(_patrollingEnemies);

        enemies = new GameObject[_patrollingEnemies];
        
        for (int i = 0; i < _patrollingEnemies; i++)
        {
            enemies[i] = Instantiate(enemyPrefab);
            enemies[i].GetComponent<Patrol>().SetPatrol(patrolRoutes[i].GetComponentsInChildren<Transform>());
        }
    }
}
