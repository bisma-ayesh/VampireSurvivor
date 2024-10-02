using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState
{
    Playing,
    Paused,
    Upgrade
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
       
    }

    public void Update()
    {
        player.PlayerMovement(); 
    }

    public void Exit()
    {
       
    }
}

public class PausedState : IState
{
    public void Enter()
    {
        Debug.Log("Entered Paused State");
    }

    public void Update()
    {
    
    }

    public void Exit()
    {
        Debug.Log("Exited Paused State");
    }
}


