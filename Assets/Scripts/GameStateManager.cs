using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    // Singleton instance
    public static GameStateManager Instance { get; private set; }

    // Make currentState private, but provide a public getter
    private IState currentState;

    // Add a public property to access the current state
    public IState CurrentState
    {
        get { return currentState; }
    }

    private void Awake()
    {
        // Ensure only one instance exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: if you want the GameStateManager to persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicates
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    private void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
