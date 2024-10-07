using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : EnemyManager
{
    // No need for 'enemyPool' here since it's inherited from EnemyManager

    protected override void Awake()
    {
        base.Awake(); // Call the base Awake method for initialization
        maxHealth = 15; // Set the maximum health for this enemy type
        moveSpeed = 5f; // Set the movement speed for this enemy type
    }



    public override void Update()
    {
        // Find the player only once to improve performance
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player")?.transform; // Safely find the player using the tag
        }

        base.Update(); // Call the base Update method to maintain movement logic
    }

    public override void MoveTowardsPlayer()
    {
        if (Player == null) return; // Exit if there is no player reference

        Vector3 direction = (Player.position - transform.position).normalized; // Calculate direction to the player
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f); // Smoothly rotate towards the player
        transform.rotation = Quaternion.LookRotation(newDirection); // Rotate towards the new direction
        transform.position += transform.forward * moveSpeed * Time.deltaTime; // Move towards the player
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet")) // Check if the collider has the "Bullet" tag
        {
            TakeDamage(2); // Inflict damage to the enemy
        }
    }

    private void OnDisable()
    {
        maxHealth = 15; // Reset health when pooled
        moveSpeed = 5f; // Reset move speed when pooled

    }
}



/*protected override void DestroyEnemy()
{
    OnEnemyDestroyed?.Invoke(transform.position, xpValue);
    base.DestroyEnemy();
    XPManager.Instance.AddXP(xpValue);
    enemyPool.ReturnEnemy(gameObject);
}*/



