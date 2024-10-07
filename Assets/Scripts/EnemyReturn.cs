using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    private ObjectPool enemyPool;
    void Start()
    {
        enemyPool = FindAnyObjectByType<ObjectPool>();
    }


    private void OnDisable () 
    {
        if (enemyPool != null)
        {
            enemyPool.ReturnEnemy(this.gameObject);
        }
    }
}
