using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefab; // Array to hold the different enemy prefabs
    [SerializeField] private int numberOfEnemiesToSpawn = 10; // Number of enemies to spawn each time

    private Transform[] spawnPoints; // Array to hold the spawn points
    private ObjectPool enemyPool; // Reference to the ObjectPool class

    void Start()
    {
        InitializeSpawnManager();
        InvokeRepeating("SpawnEnemies", 1, 1); // Start spawning enemies at intervals
    }

    private void InitializeSpawnManager()
    {
        enemyPool = FindObjectOfType<ObjectPool>(); // Find the ObjectPool in the scene
        int spawnCount = transform.childCount; // Get the number of child objects (spawn points)
        spawnPoints = new Transform[spawnCount]; // Initialize the spawn points array

        for (int i = 0; i < spawnCount; i++)
        {
            spawnPoints[i] = transform.GetChild(i); // Fill the spawn points array with the chiildren
        }
    }

    private void SpawnEnemies()
    {
        List<int> availableSpawnPoints = GetAvailableSpawnPoints(); // Gnerate a list from the mothod gatavailableSpawnPoints

        
        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            if (availableSpawnPoints.Count == 0) return; // check if there are any spawn points left in the list and exists rigt after since we are using return

            // A random position is taken from the helper method 
            int randomPosition = GetRandomSpawnPoint(availableSpawnPoints);
            int spawnPosition = availableSpawnPoints[randomPosition]; // Then this random position is used to get the actual spawn point from the available spawn points

            int enemyPrefabType = GetRandomEnemyType(); // which enemy  to spawn

            // Get an enemy from the pool
            GameObject enemy = enemyPool.GetEnemy(enemyPrefab[enemyPrefabType]);

            enemy.transform.position = spawnPoints[spawnPosition].position;

            // After spawning remove the spawnposition ie random positon is fremoved from the available spawnpoint so that no other enemies are spawned at this location in this for loop
            availableSpawnPoints.RemoveAt(randomPosition); 
        }
    }

    private List<int> GetAvailableSpawnPoints()
    {
        List<int> position = new List<int>();//empty List to store the spawn points
        for (int i = 0; i < spawnPoints.Length; i++) 
        {
            position.Add(i); //each posiotn is added into the list
        }
        return position; // Returns available spawn positions 
    }

    private int GetRandomSpawnPoint(List<int> availableSpawnIndices)
    {

        /*if (availableSpawnIndices.Count == 0)//Only to make the code robust, and handle an unexpected case
        {
            return -1; 
        }*/

        int randomPosition = Random.Range(0, availableSpawnIndices.Count); // If the list is not empty then select a positon using random range
        return randomPosition; // Return it
    }

    private int GetRandomEnemyType()
    {
        return Random.Range(0, enemyPrefab.Length); // Return a random enemy prefab list
    }
}


