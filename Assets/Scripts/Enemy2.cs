using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : EnemyManager
{
    private Transform playerTransform;


    protected void Awake()
    {
        MoveSpeed = 15f;
    }
    public override void Update()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
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

    protected override void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {

            Destroy(gameObject); 

        }
    }
}