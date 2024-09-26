using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] protected int MaxHealth = 10;
    [SerializeField] protected float MoveSpeed = 6f;
    public Transform Player;
    public float maxRadiansDelta = 2f;

    public virtual void Update()
    {
            MoveTowardsPlayer();
        
    }

    public virtual void MoveTowardsPlayer()
    {
        if (Player == null) return;

        Vector3 direction = (Player.position - transform.position).normalized;
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDirection);
        transform.position += transform.forward * MoveSpeed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
    {
        MaxHealth -= damage;
        if (MaxHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
