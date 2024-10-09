using UnityEngine;

public abstract class GameState
{
    protected GameStateManager gameStateManager;

    public GameState(GameStateManager gameStateManager)
    {
        this.gameStateManager = gameStateManager;
    }

    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}


