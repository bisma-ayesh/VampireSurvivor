using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private System.Action<HealthUpgrade> onHealthUpgrade;
    private System.Action<SpeedUpgrade> onSpeedUpgrade;

    public HealthUpgrade healthUpgrade;
    public SpeedUpgrade speedUpgrade;

    private GameStateManager gameStateManager;

    public void Setup(System.Action<HealthUpgrade> healthAction, System.Action<SpeedUpgrade> speedAction, GameStateManager stateManager)
    {
        onHealthUpgrade = healthAction;
        onSpeedUpgrade = speedAction;
        gameStateManager = stateManager;

    }
}


