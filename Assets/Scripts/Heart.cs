using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float speed;
    public float startYPosition;
    public float orbitRadius = 1f; 
    private Vector3 orbitCenter; 
    private float angle; 

    private void Start()
    {
        Vector3 position = transform.localPosition;
        position.y = startYPosition;
        transform.localPosition = position;
        orbitCenter = transform.localPosition;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyManager>().TakeDamage(5);
        }
    }

        void Update()
    {
     
        orbitCenter = transform.localPosition;

        angle += speed * Time.deltaTime;

        float xOffset = Mathf.Cos(angle) * orbitRadius;
        float zOffset = Mathf.Sin(angle) * orbitRadius;

        transform.localPosition = new Vector3(orbitCenter.x + xOffset, startYPosition, orbitCenter.z + zOffset);
    }
}
