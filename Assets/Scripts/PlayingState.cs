using UnityEngine;

public class PlayingState : GameState
{
    
  
    [SerializeField] Player player;
    

    public PlayingState(GameStateManager gameStateManager, Player player) : base(gameStateManager)
    {
        this.player = player;
    }

    public override void EnterState()
    {
        
        Debug.Log("Entered Playing State");
    }

    public override void UpdateState()
    {
        // Handle player movement
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            moveDirection = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.rotation = Quaternion.Euler(0, -90, 0);
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            player.transform.rotation = Quaternion.Euler(0, 0, 0);
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.rotation = Quaternion.Euler(0, 180, 0);
            moveDirection = Vector3.back;
        }

        player.PlayerPos.position += moveDirection.normalized * player.MoveSpeed * Time.deltaTime;

        float speed = moveDirection.magnitude;
        player.Animator.SetFloat("Speed", speed);
        player.Animator.enabled = speed > 0;

        // Switch to PausedState if "P" is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameStateManager.ChangeState(gameStateManager.pausedState);
        }
    }

    public override void ExitState()
    {
        // Any cleanup code for exiting the playing state
        Debug.Log("Exiting Playing State");
    }
}


