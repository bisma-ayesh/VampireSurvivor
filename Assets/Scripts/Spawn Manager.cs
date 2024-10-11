using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform[] _spawnPoints; // Array to hold the spawn points
    private ObjectPool _enemyPool;
    public float spawnInterval = 1.0f; // Reference to the ObjectPool class
    public int numberOfEnemiesToSpawn; // Number of enemies to spawn
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn

    void Start()
    {
        InitializeSpawnManager();
        Debug.Log("Initial spawn interval: " + spawnInterval); // Log the initial spawn interval
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
        for (int i = 0; i < numberOfEnemiesToSpawn; i++) // Use the existing numberOfEnemiesToSpawn variable
        {
            int randomPosition = GetRandomSpawnPoint(availableSpawnPoints);
            int spawnPosition = availableSpawnPoints[randomPosition];

            int enemyPrefabType = GetRandomEnemyType(); // Which enemy to spawn

            GameObject enemy = _enemyPool.GetEnemy(enemyPrefabs[enemyPrefabType]); // Use the existing enemyPrefabs array

            enemy.transform.position = _spawnPoints[spawnPosition].position;

            availableSpawnPoints.RemoveAt(randomPosition);
        }

        Debug.Log("Enemies spawned at interval: " + spawnInterval); // Log when enemies spawn
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
        return Random.Range(0, enemyPrefabs.Length); // Return a random enemy prefab index
    }

    /*public void IncreaseSpawnInterval(float amount)
    {
        spawnInterval += amount; // Increase the spawn interval
        Debug.Log("Spawn interval increased to: " + spawnInterval); // Log the updated spawn interval

        CancelInvoke("SpawnEnemies"); // Cancel the current spawn invocation
        InvokeRepeating("SpawnEnemies", 1, spawnInterval); // Restart spawning with the new interval
    }*/
    public void DecreaseSpawnInterval(float amount)
    {
        spawnInterval = Mathf.Max(0.1f, spawnInterval - amount); // Decrease the spawn interval, ensuring it doesn't go below a threshold
        Debug.Log("Spawn interval decreased to: " + spawnInterval); // Log the updated spawn interval

        CancelInvoke("SpawnEnemies"); // Cancel the current spawn invocation
        InvokeRepeating("SpawnEnemies", 1, spawnInterval); // Restart spawning with the new interval
    }
}
