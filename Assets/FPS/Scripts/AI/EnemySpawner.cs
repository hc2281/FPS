using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; 
    public Transform spawnPoint;  
    public int enemyCount = 5;     

    void SpawnEnemies()
    {
        
        for (int i = 0; i < enemyCount; i++)
        {
            
            Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);
        }
    }

    
    void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            SpawnEnemies();
        }
    }
}
