using UnityEngine;

[CreateAssetMenu(fileName = "SpawnUpgradeData", menuName = "ScriptableObjects/SpawnUpgradeData", order = 1)]
public class SpawnUpgradeData : ScriptableObject
{
    public int numberOfEnemiesToSpawn; // Number of enemies to spawn
    public GameObject[] enemyPrefabs; // Array of enemy prefabs to spawn
}