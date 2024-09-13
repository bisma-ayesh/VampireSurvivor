using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private int MaxHealth=10;
     private float MoveSpeed=6f;
     public Transform Player;
    public float maxRadiansDelta = 2f;


  

    void Start()
    {
     
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    
    void Update()
    {

        if (Player != null)
        {
            MoveTowardsPlayer();
        }
      

        

    }

   
    private void MoveTowardsPlayer()
    {
        if (Player == null) return;


        Vector3 direction = (Player.position - transform.position).normalized;

        Vector3 newDirection = Vector3.RotateTowards(transform.forward, direction, maxRadiansDelta * Time.deltaTime, 0.0f);

        transform.rotation = Quaternion.LookRotation(newDirection);

        transform.position += transform.forward * MoveSpeed * Time.deltaTime;






    }
    public void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            TakeDamage(1);
        }
    }

    public void TakeDamage(int damage)
        {
      
        
     
            Destroy(gameObject);
     
    }

}
