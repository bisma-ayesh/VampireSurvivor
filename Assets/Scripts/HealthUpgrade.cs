using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealthUpgrade", menuName = "ScriptableObjects/HealthUpgrade", order = 1)]
public class HealthUpgrade : ScriptableObject
{
    public int healthIncreaseAmount;

    public void ApplyUpgrade(Player player)
    {
        player.IncreaseHealth(healthIncreaseAmount);
    }
}
