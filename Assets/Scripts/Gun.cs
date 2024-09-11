using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject BulletPrefab;
    public float BulletSpeed;


    void Update()
    { 
        if(Input.GetKeyDown (KeyCode.Space))
    {
        GameObject bullet= Instantiate(BulletPrefab,transform.position, Quaternion.identity);
            // this lets the gamebobject bullet be manipulated without having any impact on the bullet prefab
            bullet.GetComponent<Rigidbody>().velocity = transform.up*BulletSpeed;
    }
}

}
