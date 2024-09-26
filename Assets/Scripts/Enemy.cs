using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : EnemyManager
{
    private Transform playerTransform;

    public override void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Ensure your player has the "Player" tag
        base.Update();
    }

    public override void MoveTowardsPlayer()
    {
        if (playerTransform == null) return;

        Vector3 direction = (playerTransform.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }
}
