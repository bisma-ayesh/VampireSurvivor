using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused
}

public interface IState
{
    void Enter();
    void Update();
    void Exit();
}

public class PlayingState : IState
{
    private Player player;

    public PlayingState(Player player)
    {
        this.player = player;
    }

    public void Enter()
    {
        Debug.Log("Entered Playing State");
        // Initialize game elements, start background music, etc.
    }

    public void Update()
    {
        player.PlayerMovement();
        // Handle other game logic, such as enemy AI, collision detection, etc.
    }

    public void Exit()
    {
        Debug.Log("Exited Playing State");
        // Pause game elements, stop background music, etc.
    }
}

public class PausedState : IState
{
    public void Enter()
    {
        Debug.Log("Entered Paused State");
        // Display pause menu, pause game elements, stop background music, etc.
    }

    public void Update()
    {
        // Handle pause menu input, such as resuming, quitting, or accessing options.
    }

    public void Exit()
    {
        Debug.Log("Exited Paused State");
        // Hide pause menu, resume game elements, start background music, etc.
    }
}


