using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 2;
    [SerializeField] protected float moveSpeed = 6f;
    [SerializeField] protected int xpValue = 1;
    public Transform Player;
    public float maxRadiansDelta = 2f;
    public UnityEvent<Vector3, int> OnEnemyDestroyed;



    protected ObjectPool enemyPool;

    // Reference to the player's Animator
    private Animator playerAnimator;

    public int XPValue => xpValue;

    protected virtual void Awake()
    {
        enemyPool = FindObjectOfType<ObjectPool>();
     

        // Assume the player has an Animator component and we get the reference here
        if (Player != null)
        {
            playerAnimator = Player.GetComponent<Animator>();
        }
    }

    public virtual void Update()
    {
        // Skip movement logic if paused or if Player is null
        if (Player != null)
        {
            MoveTowardsPlayer();
        }
    }

    public virtual void MoveTowardsPlayer()
    {
        if (Player == null) return;

        Vector3 direction = (Player.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward * moveSpeed * Time.deltaTime;
    }

   

    public void TakeDamage(int damage)
    {
        maxHealth -= damage;
        if (maxHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    public virtual void DestroyEnemy()
    {
        if (OnEnemyDestroyed != null)
        {
            OnEnemyDestroyed.Invoke(transform.position, xpValue);
        }
        else
        {
            Debug.Log("No listeners for OnEnemyDestroyed event.");
        }

        Debug.Log($"{gameObject.name} returned to pool.");
        enemyPool.ReturnEnemy(gameObject, xpValue);
    }
}


