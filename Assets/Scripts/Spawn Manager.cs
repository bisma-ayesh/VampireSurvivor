using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject[] enemyPrefabs;
    private Transform[] spawnpoints;
    private int spawnCount;
    [SerializeField] private int numberOfEnemiesToSpawn = 2;

  

    void Start()
    {
     
        spawnCount = transform.childCount;
        spawnpoints = new Transform[spawnCount];

        for (int i = 0; i < spawnCount; i++)
        {
            spawnpoints[i] = transform.GetChild(i);
        }

        InvokeRepeating("spawnEnemies", 5, 2);
    }

    void spawnEnemies()
    {
        List<int> Enemies = new List<int>();

        for (int i = 0; i < spawnCount; i++)
        {
            Enemies.Add(i);
        }

        for (int i = 0; i < numberOfEnemiesToSpawn; i++)
        {
            if (Enemies.Count == 0) return;

            int randomIndex = Random.Range(0, Enemies.Count);
            int spawnIndex = Enemies[randomIndex];
            int enemyPrefabIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[enemyPrefabIndex], spawnpoints[spawnIndex].position, Quaternion.identity);
            Enemies.RemoveAt(randomIndex);
           
        }


    }
}