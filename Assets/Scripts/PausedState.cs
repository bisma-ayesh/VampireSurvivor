using UnityEngine;

public class PausedState : GameState
{
    [SerializeField] private Player _player;
    

    public PausedState(GameStateManager gameStateManager, Player player)
        : base(gameStateManager)
    {
        this._player = player;
       
    }

public override void EnterState()
    {
        Debug.Log("Entered Paused State");
        Time.timeScale = 0;
    }

    public override void UpdateState()
    {
       
        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStateManager.ChangeState(GameStateManager.playingState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Paused State");

        Time.timeScale = 1;

    }
}
