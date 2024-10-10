using UnityEngine;

[CreateAssetMenu(fileName = "SpeedUpgrade", menuName = "Upgrades/SpeedUpgrade")]
public class SpeedUpgrade : ScriptableObject
{
    public float speedIncreaseAmount;

    public void ApplyUpgrade(Player player)
    {
        player.IncreaseSpeed(speedIncreaseAmount);
    }
}
