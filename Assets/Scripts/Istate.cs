using UnityEngine;

public abstract class GameState
{
    protected GameStateManager GameStateManager;

    public GameState(GameStateManager gameStateManager)
    {
        this.GameStateManager = gameStateManager;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}


