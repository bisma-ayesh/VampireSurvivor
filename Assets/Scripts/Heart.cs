using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    
    public float speed;

   
    
    void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(5);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
       
           
           Vector3 localPos = transform.localPosition;
           transform.localPosition += transform.forward * speed * Time.deltaTime;
        
        
    }
}
