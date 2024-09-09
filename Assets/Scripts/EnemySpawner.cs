using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject swarmPrefab;
    [SerializeField]
    private GameObject bigSwarmPrefab;

    [SerializeField]
    private float swarmerInterval = 3.5f;
    [SerializeField]
    private float bigSwarmInterval = 10f;

    [SerializeField]
    private int maxEnemies = 10;  // Max number of enemies

    public List<GameObject> spawnedEnemies = new List<GameObject>();  // Track spawned enemies

    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnEnemy(swarmerInterval, swarmPrefab));
        StartCoroutine(SpawnEnemy(bigSwarmInterval, bigSwarmPrefab));
    }

    private IEnumerator SpawnEnemy(float interval, GameObject enemy)
    {
        yield return new WaitForSeconds(15f);
        while (true)
        {
            yield return new WaitForSeconds(interval);

            // Only spawn new enemies if below the max limit
            if (spawnedEnemies.Count < maxEnemies)
            {
                // Random spawn position: X and Z within certain bounds, Y > 0
                Vector3 spawnPosition = new Vector3(0,0,0);
                Vector3 spawnPosition2 = new Vector3(58, 0, 58);
                Vector3 spawnPosition3 = new Vector3(58, 0, 0);//Random.Range(0f, 10f), Random.Range(0f, 10f), Random.Range(0f, 10f) will use this if we want to make random later
                GameObject newEnemy = Instantiate(enemy, spawnPosition, Quaternion.identity);
                GameObject newEnemy2 = Instantiate(enemy, spawnPosition2, Quaternion.identity);
                GameObject newEnemy3 = Instantiate(enemy, spawnPosition3, Quaternion.identity);

                // Add the newly spawned enemy to the list
                spawnedEnemies.Add(newEnemy);
                spawnedEnemies.Add(newEnemy2);
                spawnedEnemies.Add(newEnemy3);
            }
        }
    }

    // Method to remove enemy from the list
    public void RemoveEnemy(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
        }
    }
}