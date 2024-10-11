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

    private SpawnManager _spawnManager; 
    public SpawnUpgradeData spawnUpgradeData; 

    public GameState CurrentState
    {
        get { return currentState; }
    }

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>(); 
        InitializeStates();
        ChangeState(playingState); 
    }

    private void Update()
    {
        currentState?.UpdateState();
    }

    
    private void InitializeStates()
    {
        playingState = new PlayingState(this, player, _spawnManager);
        pausedState = new PausedState(this, player);
        upgradeState = new UpgradeState(this, player, _spawnManager, spawnUpgradeData) { upgradePanelPrefab = upgradePanelPrefab };
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
        
        PlayerPrefs.DeleteAll(); 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
