using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyManager
{
    
    protected override void Awake()
    {
        base.Awake();// Call the base Awake method to ensure proper initialization
       
  
       
        moveSpeed = 15f; // Set the specific move speed for Enemy2
    }

    public override void Update()
    {
        // Find the player only once to improve performance
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player")?.transform; // Find the player using the tag
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
            TakeDamage(1); // Inflict damage to the enemy
        }
    }
}
