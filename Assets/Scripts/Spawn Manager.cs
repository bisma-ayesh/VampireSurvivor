using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;  
    private Transform[] spawnpoints;  
    private int spawnCount; 
    [SerializeField] private int numberOfEnemiesToSpawn = 3; 

    void Start()
    {
        // how many spawn points we have in the children
        spawnCount=transform.childCount;

        // Define transform of the childobjects
        spawnpoints= new Transform[spawnCount];

        // takes one transform of the child and attches it to the point so for instance there are 3, the loopw will run three times.
          for(int i=0; i<spawnCount; i++)
        {
            spawnpoints[i]=transform.GetChild(i);
        }

        // Call the spawnEnemies function every 5 seconds, starting after 1 second
        InvokeRepeating("spawnEnemies", 1, 3);
    }

    void spawnEnemies()
    {
        // We want to spawn 'numberOfEnemiesToSpawn' enemies at random points


        // Create a list of available spawn points to pick from
       List<int> Enemies = new List<int>();

        // Populate the list with all spawn point indices
        for(int i =0; i<spawnCount; i++)
        {
            Enemies.Add(i);
        }

        // Now, randomly pick unique spawn points
        for(int i =0;i<numberOfEnemiesToSpawn; i++)
        {
            // Exit if no more points to pick
            if (Enemies.Count == 0) return;
            // Pick a random index from available spawn points
            int randomIndex = Random.Range(0, Enemies.Count);

            // Get the actual spawn point index from the available list
            int spawnIndex = Enemies[randomIndex];

            // Instantiate an enemy at the randomly chosen spawn point
            Instantiate(enemyPrefab, spawnpoints[spawnIndex].position, enemyPrefab.transform.rotation);

            // Remove the chosen spawn point to avoid spawning multiple enemies at the same point
            Enemies.RemoveAt(randomIndex);
        }
    }
}
