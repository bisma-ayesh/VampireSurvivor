using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : MonoBehaviour
{
    public float speed;
    public float startYPosition; 

    private void Start()
    {
       
        Vector3 position = transform.localPosition;
        position.y = startYPosition;
        transform.localPosition = position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            
            collision.GetComponent<Enemy>().TakeDamage(5);
        }
        else
        {
            StartCoroutine(DestroyAfterDelay(2f));
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Update()
    {
       
        Vector3 localPos = transform.localPosition;
        transform.localPosition += transform.forward * speed * Time.deltaTime;
    }
}
