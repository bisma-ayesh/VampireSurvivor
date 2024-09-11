using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxHeath;
    [SerializeField] private Transform playerPos;
    

    private int Health;

    
        
    void Start()
    {
        Health=MaxHeath;
    }

    
   void Update()

    {
        //Movement over time and Input Getkey, right arrow adds one and left arrows substracts one. ? is the conditional operator (short end way to write if and else statements)

        Vector3 moveDirection = new Vector3(
            (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0),
            0,
            (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0)
        );
        playerPos.position += moveDirection.normalized * MoveSpeed * Time.deltaTime;


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
