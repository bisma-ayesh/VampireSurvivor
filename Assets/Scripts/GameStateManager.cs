using UnityEngine;

public class GameStateManager : MonoBehaviour
{
  
    public static GameStateManager Instance { get; private set; }

    private IState currentState;

    public IState CurrentState
    {
        get { return currentState; }
    }

    private void Awake()
    {
       
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject); 
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
