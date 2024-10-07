using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] protected int maxHealth = 2; // Maximum health of the enemy
    [SerializeField] protected float moveSpeed = 6f; // Movement speed of the enemy
    [SerializeField] protected int xpValue = 1; // Experience points given on enemy defeat
    public Transform Player; // Reference to the player's transform
    public float maxRadiansDelta = 2f; // Maximum rotation speed towards the player
    public UnityEvent<Vector3, int> OnEnemyDestroyed; // Event invoked when the enemy is destroyed

    protected ObjectPool enemyPool; // Reference to the enemy pool

    public int XPValue => xpValue; // Expose XP value for use in ObjectPool

    protected virtual void Awake()
    {
        enemyPool = FindObjectOfType<ObjectPool>(); // Find the ObjectPool instance in the scene
    }

    public virtual void Update()
    {
        MoveTowardsPlayer(); // Move the enemy towards the player each frame
    }

    public virtual void MoveTowardsPlayer()
    {
        if (Player == null) return; // Exit if the player reference is missing

        Vector3 direction = (Player.position - transform.position).normalized; // Calculate direction to player
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f); // Smooth rotation
        transform.rotation = Quaternion.LookRotation(newDirection); // Rotate towards player
        transform.position += transform.forward * moveSpeed * Time.deltaTime; // Move towards player
    }

    public void TakeDamage(int damage)
    {
        maxHealth -= damage; // Decrease health by the damage amount
        if (maxHealth <= 0)
        {
            DestroyEnemy(); // Call method to handle enemy destruction
        }
    }

    public virtual void DestroyEnemy()
    {
        // Check if there are any listeners and invoke the OnEnemyDestroyed event
        if (OnEnemyDestroyed != null)
        {
            // Invoke the event, passing the position and XP value
            OnEnemyDestroyed.Invoke(transform.position, xpValue);

        }
        else
        {
            // Log if there are no listeners to indicate the event won't be invoked
            Debug.Log("No listeners for OnEnemyDestroyed event.");
        }

        // Debug log for tracking enemy return
        Debug.Log($"{gameObject.name} returned to pool.");

        // Return the enemy instance to the pool instead of destroying it
        enemyPool.ReturnEnemy(gameObject, xpValue); // Pass the XP value when returning
    }

    /*private void SubscribeToXPManager()
    {
        // Ensure this enemy subscribes to the XPManager when instantiated
        if (XPManager.Instance != null)
        {
            OnEnemyDestroyed.AddListener(XPManager.Instance.HandleEnemyDestroyed);
        }
    }*/
}
