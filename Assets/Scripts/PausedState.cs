using UnityEngine;

public class PausedState : GameState
{
    [SerializeField] private Player player;
    /*[SerializeField] private GameObject enemyManager;
    [SerializeField] private GameObject objectPool; // Reference to the Object Pool GameObject
    [SerializeField] private GameObject spawnManager; // Reference to the Spawn Manager GameObject
    [SerializeField] private GameObject xpManager; // Reference to the XP Manager GameObject*/

    public PausedState(GameStateManager gameStateManager, Player player)
        : base(gameStateManager)
    {
        this.player = player;
        /*this.enemyManager = enemyManager;
        this.objectPool = objectPool;
        this.spawnManager = spawnManager;
        this.xpManager = xpManager;*/
    }

public override void EnterState()
    {
        Debug.Log("Entered Paused State");

        // Freeze the game by setting time scale to 0
        Time.timeScale = 0;

        //Freeze the player and disable movement
         //player.Animator.SetFloat("Speed", 0);
         //player.Animator.enabled = false;

         /*if (enemyManager != null)
         {
             enemyManager.SetActive(false); // Disable EnemyManager updates
             Debug.Log("EnemyManager is " + enemyManager);
         }

         if (objectPool != null)
         {
             objectPool.SetActive(false); // Disable Object Pool
             Debug.Log("Object Pool is " + objectPool);
         }

         if (spawnManager != null)
         {
             spawnManager.SetActive(false); // Disable Spawn Manager
             Debug.Log("Spawn Manager is " + spawnManager);
         }

         if (xpManager != null)
         {
             xpManager.SetActive(false); // Disable XP Manager
             Debug.Log("XP Manager is " + xpManager);
         }*/
    }

    public override void UpdateState()
    {
        // Switch back to PlayingState if "P" is pressed
        if (Input.GetKeyDown(KeyCode.P))
        {
            gameStateManager.ChangeState(gameStateManager.playingState);
        }
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Paused State");

        // Reactivate various managers when exiting paused state
        /*if (enemyManager != null)
        {
            enemyManager.SetActive(true); // Reactivate EnemyManager GameObject
        }

        if (objectPool != null)
        {
            objectPool.SetActive(true); // Reactivate Object Pool
        }

        if (spawnManager != null)
        {
            spawnManager.SetActive(true); // Reactivate Spawn Manager
        }

        if (xpManager != null)
        {
            xpManager.SetActive(true); // Reactivate XP Manager
        }*/

        // Resume the game by setting time scale back to 1
        Time.timeScale = 1;

        // Re-enable Animator
        //player.Animator.enabled = true;
    }
}
