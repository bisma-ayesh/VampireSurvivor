using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float MoveSpeed;
    [SerializeField] private int MaxHeath;

    private int Health;

    void Start()
    {
        Health=MaxHeath;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(1, 0, 0) * MoveSpeed * Time.deltaTime;  
    }

    public void TakeDamage(int someDamage)
    {
        Health-= someDamage;
        if (Health < 0) Death();
    }

    public void Death()
    {
        Console.WriteLine("You died!");
    }
}
