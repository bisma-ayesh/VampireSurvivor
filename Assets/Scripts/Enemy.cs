using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : EnemyManager
{
    private Transform playerTransform;


    protected void Awake()
    {
        MaxHealth = 15;
        MoveSpeed = 5f;
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
            TakeDamage(2);
        }
    }
    protected override void DestroyEnemy()
    {
        OnEnemyDestroyed?.Invoke(transform.position, xpValue);
        base.DestroyEnemy();
        XPManager.Instance.AddXP(xpValue);
        Debug.Log("Line execute");
        Destroy(gameObject);
    }


}

