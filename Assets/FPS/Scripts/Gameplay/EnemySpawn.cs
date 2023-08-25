using System.Collections;
using UnityEngine;

namespace Unity.FPS.Gameplay
{
    public class EnemySpawn : MonoBehaviour
    {
        public GameObject enemyPrefab; // Assign your enemy prefab here
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
        public float zMin;
        public float zMax;
        public int EnemyCount;

        private bool hasSpawned = false;

        private void OnTriggerEnter(Collider other)
        {
            if (hasSpawned) return;

            var player = other.GetComponent<PlayerCharacterController>();
            if (player != null)
            {
                StartCoroutine(SpawnEnemies());
                hasSpawned = true; // Ensure the enemies only spawn once
                //Debug.Log(other + " entered the trigger area!");
            }
        }

        IEnumerator SpawnEnemies()
        {
            int i = 0;
            while (true) // Infinite loop, but we'll break out of it once we've spawned enough enemies
            {
                string currentDifficulty = DifficultyController.Instance.DifficultyLevel;
                switch (currentDifficulty)
                {
                    case "easy":
                        EnemyCount = 5;
                        break;
                    case "medium":
                        EnemyCount = 10;
                        break;
                    case "hard":
                        EnemyCount = 20;
                        break;
                }

                if (i >= EnemyCount)
                    break; // Exit the loop if we've spawned enough enemies

                float xPos = Random.Range(xMin, xMax);
                float yPos = Random.Range(yMin, yMax);
                float zPos = Random.Range(zMin, zMax);
                Instantiate(enemyPrefab, new Vector3(xPos, yPos, zPos), Quaternion.identity);
                yield return new WaitForSeconds(0.1f);
                i += 1;
            }
            //Debug.Log("Have generated " + i + " enemies.");
        }
    }
}
