using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float speed;
    public Transform target;
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy");
        {
            collision.GetComponent<Enemy>().TakeDamage(5);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(target.position,Vector3.forward,speed);
    }
}
