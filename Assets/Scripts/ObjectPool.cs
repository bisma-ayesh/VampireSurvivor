using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    // Dictionary to hold pools for different enemy types based on their prefab names
    private Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();

    // Reference to the XPManager
    private XPManager xpManager;

    private void Awake()
    {
        xpManager = XPManager.Instance; // Get the XPManager instance
    }

    // Retrieve an enemy from the pool or create a new one if the pool is empty
    public GameObject GetEnemy(GameObject enemyPrefab)
    {
        // Try to get the pool associated with this enemyPrefab
        if (enemyPool.TryGetValue(enemyPrefab.name, out Queue<GameObject> enemyPoolList))
        {
            // If the pool is empty, create a new enemy
            if (enemyPoolList.Count == 0)
            {
                return CreateNewEnemy(enemyPrefab);
            }
            else
            {
                // Dequeue an existing enemy from the pool, activate it, and return it
                GameObject enemy = enemyPoolList.Dequeue();
                enemy.SetActive(true);
                return enemy;
            }
        }
        else
        {
            // If no pool exists for this prefab, create a new enemy
            return CreateNewEnemy(enemyPrefab);
        }
    }

    // Create a new enemy and assign it to the pool
    private GameObject CreateNewEnemy(GameObject enemyPrefab)
    {
        GameObject instantiatedEnemy = Instantiate(enemyPrefab); // Instantiate a new enemy
        instantiatedEnemy.name = enemyPrefab.name; // Set its name to the prefab's name
        return instantiatedEnemy; // Return the newly created enemy
    }

    // Return an enemy to the pool
    public void ReturnEnemy(GameObject enemyInstance, int xpValue) // Added xpValue parameter
    {
        // Try to get the pool associated with the enemy's name
        if (enemyPool.TryGetValue(enemyInstance.name, out Queue<GameObject> enemyPoolList))
        {
            // Add the enemy back to the pool
            enemyPoolList.Enqueue(enemyInstance);
        }
        else
        {
            // If no pool exists for this enemy type, create a new pool
            Queue<GameObject> newPool = new Queue<GameObject>();
            newPool.Enqueue(enemyInstance);
            enemyPool.Add(enemyInstance.name, newPool); // Add the new pool to the dictionary
        }

        // Deactivate the enemy before returning it to the pool
        enemyInstance.SetActive(false);

        // Call XPManager to add XP when the enemy is returned to the pool
        if (xpManager != null)
        {
            EnemyManager enemyManager = enemyInstance.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                xpManager.HandleEnemyDestroyed(enemyInstance.transform.position, enemyManager.XPValue);
            }
        }
    }
}



