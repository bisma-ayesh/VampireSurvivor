using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : EnemyManager
{
    protected override void Awake()
    {
        base.Awake(); 
        maxHealth = 15; 
        moveSpeed = 5f; 
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

    private void OnTriggerEnter(Collider _collision)
    {
        if (_collision.CompareTag("Bullet")) 
        {
            TakeDamage(2); 
        }
    }

    private void OnDisable()
    {
        maxHealth = 15; 
        moveSpeed = 5f; 
    }

    public override void DestroyEnemy()
    {
        
        OnEnemyDestroyed?.Invoke(transform.position, xpValue);
        base.DestroyEnemy();
        XPManager.Instance.AddXP(xpValue);
        ScoreManager.Instance.AddScore(xpValue);
        enemyPool.ReturnEnemy(gameObject, xpValue);
    }

}
