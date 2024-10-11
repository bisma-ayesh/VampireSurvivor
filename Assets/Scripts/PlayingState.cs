using UnityEngine;

public class PlayingState : GameState
{
    [SerializeField] private Player _player;
    [SerializeField] private SpawnManager _spawnManager; 

    public PlayingState(GameStateManager gameStateManager, Player player, SpawnManager spawnManager) : base(gameStateManager)
    {
        this._player = player;
        this._spawnManager = spawnManager; 
    }

    public override void EnterState()
    {
        Debug.Log("Entered Playing State");

        if (XPManager.Instance == null)
        {
            Debug.LogError("XPManager.Instance is null. Subscription failed.");
        }
        else
        {
            XPManager.Instance.OnLevelUp += HandleLevelUp;
            Debug.Log($"Subscribed to OnLevelUp event: {nameof(HandleLevelUp)}");
        }
    }


    public override void UpdateState()
    {
       
        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKey(KeyCode.RightArrow))
        {
            _player.transform.rotation = Quaternion.Euler(0, 90, 0);
            moveDirection = Vector3.right;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            _player.transform.rotation = Quaternion.Euler(0, -90, 0);
            moveDirection = Vector3.left;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            _player.transform.rotation = Quaternion.Euler(0, 0, 0);
            moveDirection = Vector3.forward;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
             _player.transform.rotation = Quaternion.Euler(0, 180, 0);
            moveDirection = Vector3.back;
        }

        _player.playerPos.position += moveDirection.normalized * _player.moveSpeed * Time.deltaTime;

        float speed = moveDirection.magnitude;
        _player.animator.SetFloat("Speed", speed);
        _player.animator.enabled = speed > 0;


        if (Input.GetKeyDown(KeyCode.P))
        {
            GameStateManager.ChangeState(GameStateManager.pausedState);
        }
    }

    private void HandleLevelUp(int _newLevel)
    {
        Debug.Log($"Player leveled up to level {_newLevel}. Switching to UpgradeState.");
        GameStateManager.ChangeState(GameStateManager.upgradeState);
    }

    public override void ExitState()
    {
        
        XPManager.Instance.OnLevelUp -= HandleLevelUp;
        Debug.Log("Exiting Playing State");
    }
}
