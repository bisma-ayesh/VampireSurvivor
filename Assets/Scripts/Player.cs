using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxHeath;
    [SerializeField] private Transform PlayerPos;
    private bool walkingRight = true;

    private int Health;

    void Start()
    {
        Health=MaxHeath;
    }

    
   void Update()

    {
       
         PlayerPos.position += new Vector3 (1,0,0) * MoveSpeed * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            SwitchDirection();
        }

    }
  

    private void SwitchDirection()
    {
        {
           

            walkingRight = !walkingRight;

            if (walkingRight)
            {
                PlayerPos.rotation = Quaternion.Euler(0, 45, 0);
            }
            else
            {
                PlayerPos.rotation = Quaternion.Euler(0, -45, 0);
            }
        }
    }
    public void TakeDamage(int someDamage)
    {
        Health-= someDamage; // same thing as Health= Health-someDamage;
        if (Health < 0) Death();
    }

    public void Death()
    {
        Console.WriteLine("You died!");
    }
}
