using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    private GameState currentState;

    public Player player; // Reference to the Player class instance

    public PlayingState playingState;
    public PausedState pausedState;
    public UpgradeState upgradeState;
    public GameObject upgradePanelPrefab;

    // public HealthUpgrade availableUpgrades; // Add this to hold available upgrades
    // public GameObject upgradeButtonPrefab; // Reference to the Upgrade Button prefab
    // public Transform upgradeButtonParent;


    //[SerializeField] private GameObject enemyManager; // Reference to EnemyManager
    //[SerializeField] private GameObject objectPool; // Reference to the Object Pool GameObject
    //[SerializeField] private GameObject spawnManager; // Reference to the Spawn Manager GameObject
    // [SerializeField] private GameObject xpManager; // Reference to the XP Manager GameObject

    public GameState CurrentState // Add this public property
    {
        get { return currentState; }
    }

    private void Start()
    {
        // Initialize all states and set the initial state to PlayingState
        playingState = new PlayingState(this, player); // Pass the Player instance

        // Pass the required GameObjects to the PausedState constructor
        pausedState = new PausedState(this, player);

        upgradeState = new UpgradeState(this, player) { upgradePanelPrefab = upgradePanelPrefab };

        // Initialize the UpgradeState with the necessary UI references
        // upgradeState = new UpgradeState(this, player, availableUpgrades)
        //{
        // upgradeButtonPrefab = upgradeButtonPrefab,
        //  upgradeButtonContainer = upgradeButtonParent
        // };

        // Set the initial state
        ChangeState(playingState); // Start with the Playing state
    }

    private void Update()
    {
        // Ensure the current state is not null before updating
        currentState?.UpdateState();
    }

    public void ChangeState(GameState newState)
    {
        // Check if the newState is valid
        if (newState == null)
        {
            Debug.LogError("Attempted to change to a null state.");
            return;
        }

        // Exit the current state if it exists
        currentState?.ExitState();

        // Update to the new state
        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }
}
