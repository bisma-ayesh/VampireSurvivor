using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxHeath;
    [SerializeField] private Transform PlayerPos;
    [SerializeField] private Transform Heart;
    [SerializeField] private Transform Orbiter;
    [SerializeField] private float OrbitRadius;
    [SerializeField] private float OrbitVelocity;


    private int Health;
    private float CurrentAngle;

    
        
    void Start()
    {
        Health=MaxHeath;
    }

    
   void Update()

    {
        //Movement over time and Input Getkey, right arrow adds one and left arrows substracts one. ? is the conditional operator (short end way to write if and else statements)

        OrbiterMovement();

        Vector3 moveDirection = new Vector3(
            (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) + (Input.GetKey(KeyCode.LeftArrow) ? -1 : 0),
            0,
            (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) + (Input.GetKey(KeyCode.DownArrow) ? -1 : 0)
        );
        PlayerPos.position += moveDirection.normalized * MoveSpeed * Time.deltaTime;

        Vector3 Heartdirection = (PlayerPos.position - (Vector3)transform.position);
        Heart.up = Heartdirection.normalized;



    }

    void OrbiterMovement()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CurrentAngle += OrbitVelocity * Time.deltaTime;
            Vector3 orbitPos = new Vector3(Mathf.Cos(Time.time), Mathf.Sin(Time.time), 0) * OrbitRadius;
            Orbiter.localPosition = orbitPos;

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
