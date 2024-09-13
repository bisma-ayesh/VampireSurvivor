using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private int MaxHealth=10;
     private float MoveSpeed=5f;
    public Transform Player;
   
  

    private int Health;

    void Start()
    {
        
        Health=MaxHealth;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
       
    }

    
    void Update()
    {

       //check if the reference is crooect and then envoke the method
        if (Player != null)
        {
            MoveTowardsPlayer();
        }
      
        //On collison minus one health
        //if health is less the 0 the destroy  if (player != null)
        

    }

   
    private void MoveTowardsPlayer()
    {
        if (Player == null) return;

        Vector3 direction=(Player.position-transform.position).normalized;

        transform.position += direction * MoveSpeed * Time.deltaTime;

        

        

        
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
        //Health -= damage;
       // if (Health < 0)
        
        //{
            Destroy(gameObject);
        //}
    }

}
