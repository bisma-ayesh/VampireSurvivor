using UnityEngine;

public class UpgradeState : GameState
{
    private Player player;
    public GameObject upgradePanelPrefab; // Reference to the upgrade UI prefab
    private GameObject upgradePanel; // Store a reference to the instantiated panel for cleanup
    private UpgradeButton panel;

    // References for SpawnManager and SpawnUpgradeData
    private SpawnManager spawnManager;
    private SpawnUpgradeData spawnUpgradeData;

    public UpgradeState(GameStateManager gameStateManager, Player player, SpawnManager spawnManager, SpawnUpgradeData spawnUpgradeData) : base(gameStateManager)
    {
        this.player = player;
        this.spawnManager = spawnManager;
        this.spawnUpgradeData = spawnUpgradeData;
    }

    public override void EnterState()
    {
        Time.timeScale = 0; // Pause the game
        upgradePanel = Object.Instantiate(upgradePanelPrefab); // Instantiate the UI
        panel = upgradePanel.GetComponent<UpgradeButton>();

        // Load the Scriptable Objects
        HealthUpgrade healthUpgrade = Resources.Load<HealthUpgrade>("HealthUpgrade");
        SpeedUpgrade speedUpgrade = Resources.Load<SpeedUpgrade>("SpeedUpgrade");

        // Setup the panel with the upgrade actions and scriptable objects
        panel.healthUpgrade = healthUpgrade; // Assign the health upgrade
        panel.speedUpgrade = speedUpgrade; // Assign the speed upgrade
        panel.Setup(OnHealthUpgradeSelected, OnSpeedUpgradeSelected, gameStateManager); // Set up the panel
    }

    public override void UpdateState()
    {
        // Handle input logic for upgrades within the UpgradeState
        if (Input.GetKeyDown(KeyCode.A))
        {
            OnHealthUpgradeSelected(panel.healthUpgrade);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            OnSpeedUpgradeSelected(panel.speedUpgrade);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Exit to PlayingState without upgrading
            ExitToPlayingState();
        }
    }

    private void OnHealthUpgradeSelected(HealthUpgrade healthUpgrade)
    {
        healthUpgrade.ApplyUpgrade(player);
        UpdateSpawnData(); // Call to update spawn data if necessary
        ExitToPlayingState(); // Exit after applying the upgrade
    }

    private void OnSpeedUpgradeSelected(SpeedUpgrade speedUpgrade)
    {
        speedUpgrade.ApplyUpgrade(player);
        UpdateSpawnData(); // Call to update spawn data if necessary
        ExitToPlayingState(); // Exit after applying the upgrade
    }

    private void UpdateSpawnData()
    {
        // Example of modifying the spawn data based on upgrades
        if (spawnUpgradeData != null)
        {
            spawnUpgradeData.numberOfEnemiesToSpawn += 200; // Example upgrade logic
            // You can also add logic to modify enemy prefabs or other properties
        }
    }

    public override void ExitState()
    {
        // Cleanup: Destroy the UI
        if (upgradePanel != null)
        {
            Object.Destroy(upgradePanel);
        }
    }

    private void ExitToPlayingState()
    {
        ExitState(); // Call exit state to clean up UI
        Time.timeScale = 1; // Resume the game time
        gameStateManager.ChangeState(gameStateManager.playingState); // Change back to the PlayingState
    }
}
