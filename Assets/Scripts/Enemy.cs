using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private int MaxHealth;
     private float MoveSpeed;

    private int Health;

    void Start()
    {
        Health=MaxHealth;
    }

    
    void Update()
    {
        //move towards the player
        //On collison minus one health
        //if health is less the 0 the destroy enemy
    }

    public void OnCollison()
    {

    }

    public void Destroy()
        {
    }

}
