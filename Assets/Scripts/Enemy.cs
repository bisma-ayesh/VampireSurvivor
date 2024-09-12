using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
     private int MaxHealth;
     private float MoveSpeed;
    public GameObject prefab;
    public Transform[] spawnPoint;
    private Vector3 spawnPos;
  

    private int Health;

    void Start()
    {
        
        Health=MaxHealth;
       
    }

    
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SpawnEnemy();
        }
        //move towards the player
        //On collison minus one health
        //if health is less the 0 the destroy  if (player != null)
        

    }

    public void SpawnEnemy()

    {
        Instantiate(prefab, spawnPos, Quaternion.identity);
    }

    public void OnCollison()
    {

    }

    public void Destroy()
        {
    }

}
