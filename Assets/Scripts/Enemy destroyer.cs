using UnityEngine;

public class EnemyDestroyer : MonoBehaviour
{
    [SerializeField]
    private EnemySpawner enemySpawner;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is an enemy
        if (other.CompareTag("Enemy"))
        {
            // Remove the enemy from the spawner's list
            enemySpawner.RemoveEnemy(other.gameObject);

            // Destroy the enemy GameObject
            Destroy(other.gameObject);
        }
    }
}