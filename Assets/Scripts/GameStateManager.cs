using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    private GameState currentState;

    public Player player;

    public PlayingState playingState;
    public PausedState pausedState;
    public UpgradeState upgradeState;
    public GameObject upgradePanelPrefab;
    public GameObject gameOverUI;

    private SpawnManager spawnManager; // Define spawnManager
    public SpawnUpgradeData spawnUpgradeData; // Reference to the SpawnUpgradeData ScriptableObject

    public GameState CurrentState
    {
        get { return currentState; }
    }

    private void Start()
    {
        spawnManager = FindObjectOfType<SpawnManager>(); // Find the SpawnManager in the scene
        InitializeStates();
        ChangeState(playingState); // Start in the playing state
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    // Helper method to initialize states
    private void InitializeStates()
    {
        playingState = new PlayingState(this, player, spawnManager);
        pausedState = new PausedState(this, player);
        upgradeState = new UpgradeState(this, player, spawnManager, spawnUpgradeData) { upgradePanelPrefab = upgradePanelPrefab };
    }

    public void ChangeState(GameState newState)
    {
        if (newState == null)
        {
            Debug.LogError("Attempted to change to a null state.");
            return;
        }

        currentState?.ExitState();
        currentState = newState;
        currentState.EnterState();
    }

    public void GameOver()
    {
        gameOverUI.SetActive(true);
    }

    public void Restart()
    {
        // Reload the current scene
        PlayerPrefs.DeleteAll(); // Clear all saved data
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
