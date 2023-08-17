using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public DifficultyController difficultyController;
    public GameObject enemyPrefab; // Assign your enemy prefab here
    public Transform detectionArea; // This can be a trigger collider around the area you want to detect the player
    public float spawnRadius; // Radius within which enemies will spawn randomly around the spawner
    private bool playerDetected = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by: " + other.gameObject.name); // This will show you which GameObject entered the trigger
        if (!playerDetected && other.CompareTag("Player"))
        {
            Debug.Log("Player detected!"); // This will confirm if the player was detected
            playerDetected = true;
            SpawnEnemies();
        }
    }

    private void SpawnEnemies()
    {
        int enemyCount = 0;

        switch (difficultyController.DifficultyLevel)
        {
            case "easy":
                enemyCount = 5;
                break;
            case "medium":
                enemyCount = 10;
                break;
            case "hard":
                enemyCount = 20;
                break;
        }

        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 spawnPosition = GetRandomSpawnPosition();
            Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetRandomSpawnPosition()
    {
        Vector3 randomOffset = new Vector3(
            Random.Range(-spawnRadius, spawnRadius),
            0,  // Assuming you still want to spawn on a flat surface
            Random.Range(-spawnRadius, spawnRadius)
        );

        return transform.position + randomOffset;
    }
}
