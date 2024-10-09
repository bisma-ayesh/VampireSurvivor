using UnityEngine;

//public class UpgradeState : GameState // Assuming UpgradeState inherits from GameState
/*{
    /*private Player player; // Declare the player variable
    private EnemyManager enemyManager;
    private XPManager xpManager;
    

    public UpgradeState(GameStateManager gameStateManager, Player player, EnemyManager enemyManager, XPManager xpManager)
        : base(gameStateManager)
    {
        this.player = player; // Store the reference
        this.enemyManager = enemyManager; // Store the reference
        this.xpManager = xpManager; // Store the reference
    }

    public override void EnterState()
    {
        Debug.Log("Entered Upgrade State");

        // Use passed references
        enemyManager.OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        xpManager.OnLevelUp += HandleLevelUp;
    }

    public override void UpdateState()
    {
        // Handle logic for upgrading the player
        // For example, detecting upgrade choices, applying upgrades, etc.
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Upgrade State");

        // Unsubscribe from events to prevent memory leaks
        enemyManager.OnEnemyDestroyed.RemoveListener(HandleEnemyDestroyed);
        xpManager.OnLevelUp -= HandleLevelUp;
    }

    private void HandleEnemyDestroyed(Vector3 enemyPosition, int xpGained)
    {
        // Add XP to the player
        xpManager.AddXP(xpGained);
    }

    private void HandleLevelUp(int newLevel)
    {
        // Increase player speed based on the new level
        IncreasePlayerSpeed(newLevel);
    }

    private void IncreasePlayerSpeed(int newLevel)
    {
        // Assuming MoveSpeed and SpeedIncreasePerLevel are properties of Player
        player.MoveSpeed += player.SpeedIncreasePerLevel; // Adjust based on your Player implementation
    }*/


