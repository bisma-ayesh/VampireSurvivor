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

        // Check if XPManager.Instance is null to ensure it's initialized
        if (XPManager.Instance == null)
        {
            Debug.LogError("XPManager.Instance is null. Subscription failed.");
        }
        else
        {
            // Subscribe to the level-up event from XPManager
            XPManager.Instance.OnLevelUp += HandleLevelUp;
            Debug.Log($"Subscribed to OnLevelUp event: {nameof(HandleLevelUp)}");
        }
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

    // Event handler for level-up
    private void HandleLevelUp(int newLevel)
    {
        Debug.Log($"Player leveled up to level {newLevel}. Switching to UpgradeState.");

        // Switch to the UpgradeState
        gameStateManager.ChangeState(gameStateManager.upgradeState);
    }

    public override void ExitState()
    {
        // Unsubscribe from the level-up event to avoid memory leaks
        XPManager.Instance.OnLevelUp -= HandleLevelUp;

        // Any cleanup code for exiting the playing state
        Debug.Log("Exiting Playing State");
    }
}


