using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private SpawnUpgradeData spawnUpgradeData; // Reference to the SpawnUpgradeData
    private Transform[] _spawnPoints; // Array to hold the spawn points
    private ObjectPool _enemyPool;
    [SerializeField] private float spawnInterval = 1.0f;// Reference to the ObjectPool class

    void Start()
    {
        InitializeSpawnManager();
        InvokeRepeating("SpawnEnemies", 1, spawnInterval); // Start spawning enemies at intervals
    }

    private void InitializeSpawnManager()
    {
        _enemyPool = FindObjectOfType<ObjectPool>(); // Find the ObjectPool in the scene
        int spawnCount = transform.childCount; // Get the number of child objects (spawn points)
        _spawnPoints = new Transform[spawnCount]; // Initialize the spawn points array

        for (int i = 0; i < spawnCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i); // Fill the spawn points array with the children
        }
    }

    public void SpawnEnemies()
    {
        List<int> availableSpawnPoints = GetAvailableSpawnPoints(); // Generate a list from the method GetAvailableSpawnPoints

        for (int i = 0; i < spawnUpgradeData.numberOfEnemiesToSpawn; i++)
        {
            if (availableSpawnPoints.Count == 0) return; // Check if there are any spawn points left in the list

            // A random position is taken from the helper method 
            int randomPosition = GetRandomSpawnPoint(availableSpawnPoints);
            int spawnPosition = availableSpawnPoints[randomPosition]; // Get the actual spawn point

            int enemyPrefabType = GetRandomEnemyType(); // Which enemy to spawn

            // Get an enemy from the pool
            GameObject enemy = _enemyPool.GetEnemy(spawnUpgradeData.enemyPrefabs[enemyPrefabType]);

            enemy.transform.position = _spawnPoints[spawnPosition].position;

            // Remove the spawn position so that no other enemies are spawned at this location in this for loop
            availableSpawnPoints.RemoveAt(randomPosition);
        }
    }

    private List<int> GetAvailableSpawnPoints()
    {
        List<int> position = new List<int>(); // Empty List to store the spawn points
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            position.Add(i); // Each position is added into the list
        }
        return position; // Returns available spawn positions 
    }

    private int GetRandomSpawnPoint(List<int> availableSpawnIndices)
    {
        int randomPosition = Random.Range(0, availableSpawnIndices.Count); // Select a position using random range
        return randomPosition; // Return it
    }

    private int GetRandomEnemyType()
    {
        return Random.Range(0, spawnUpgradeData.enemyPrefabs.Length); // Return a random enemy prefab index
    }
    public void IncreaseSpawnInterval(float amount)
    {
        spawnInterval += amount; // Increase the spawn interval
        CancelInvoke("SpawnEnemies"); // Cancel the current spawn invocation
        InvokeRepeating("SpawnEnemies", 1, spawnInterval); // Restart spawning with the new interval
    }
}


