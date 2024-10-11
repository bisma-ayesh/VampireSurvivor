using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private Transform[] _spawnPoints; 
    private ObjectPool _enemyPool;
    public float spawnInterval = 1.0f; 
    public int numberOfEnemiesToSpawn; 
    public GameObject[] enemyPrefabs;

    void Start()
    {
        InitializeSpawnManager();
        Debug.Log("Initial spawn interval: " + spawnInterval);
        InvokeRepeating("SpawnEnemies", 1, spawnInterval);
    }

    private void InitializeSpawnManager()
    {
        _enemyPool = FindObjectOfType<ObjectPool>(); 
        int spawnCount = transform.childCount;
        _spawnPoints = new Transform[spawnCount];
        for (int i = 0; i < spawnCount; i++)
        {
            _spawnPoints[i] = transform.GetChild(i); 
        }
    }

    public void SpawnEnemies()
    {
        List<int> availableSpawnPoints = GetAvailableSpawnPoints(); 
        for (int i = 0; i < numberOfEnemiesToSpawn; i++) 
        {
            if (availableSpawnPoints.Count == 0) break; 

            int randomPositionIndex = GetRandomSpawnPoint(availableSpawnPoints);
            int spawnPosition = availableSpawnPoints[randomPositionIndex];

            int enemyPrefabType = GetRandomEnemyType(); 

            GameObject enemy = _enemyPool.GetEnemy(enemyPrefabs[enemyPrefabType]); 

            
            enemy.transform.position = _spawnPoints[spawnPosition].position;

            
            Rigidbody rb = enemy.GetComponent<Rigidbody>();
            if (rb == null) 
            {
                rb = enemy.AddComponent<Rigidbody>(); 
            }
            rb.isKinematic = true; 

            availableSpawnPoints.RemoveAt(randomPositionIndex);
        }

        Debug.Log("Enemies spawned at interval: " + spawnInterval); 
    }

    private List<int> GetAvailableSpawnPoints()
    {
        List<int> position = new List<int>(); 
        for (int i = 0; i < _spawnPoints.Length; i++)
        {
            position.Add(i); 
        }
        return position; 
    }

    private int GetRandomSpawnPoint(List<int> availableSpawnIndices)
    {
        int randomPosition = Random.Range(0, availableSpawnIndices.Count); 
        return randomPosition; 
    }

    private int GetRandomEnemyType()
    {
        return Random.Range(0, enemyPrefabs.Length); 
    }

    public void DecreaseSpawnInterval(float amount)
    {
        spawnInterval = Mathf.Max(0.1f, spawnInterval - amount);
        Debug.Log("Spawn interval decreased to: " + spawnInterval);

        CancelInvoke("SpawnEnemies");
        InvokeRepeating("SpawnEnemies", 1, spawnInterval);
    }
}
