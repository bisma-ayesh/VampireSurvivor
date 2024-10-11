using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyManager
{
    
    protected override void Awake()
    {
        base.Awake();
        moveSpeed = 15f; 
    }

    public override void Update()
    {

        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player")?.transform; 
        }
        base.Update(); 
    }

    public override void MoveTowardsPlayer()
    {
        if (Player == null) return; 

        Vector3 direction = (Player.position - transform.position).normalized; 
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f); 
        transform.rotation = Quaternion.LookRotation(newDirection); 
        transform.position += transform.forward * moveSpeed * Time.deltaTime; 
    }
 

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet")) 
        {
            TakeDamage(1); 
        }
    }
}
