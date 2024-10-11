using UnityEngine;

[CreateAssetMenu(fileName = "SpawnUpgradeData", menuName = "ScriptableObjects/SpawnUpgradeData", order = 1)]
public class SpawnUpgradeData : ScriptableObject
{
    public int decreaseSpawnAmount;

    public void UpdateSpawndData(SpawnManager spawnManager)
    {
        Debug.Log("Updating spawn data, increasing spawn interval by: " + decreaseSpawnAmount); 
        spawnManager.DecreaseSpawnInterval(decreaseSpawnAmount); 
    }
}
