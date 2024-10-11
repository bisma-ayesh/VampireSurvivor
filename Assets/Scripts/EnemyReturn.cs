using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyReturn : MonoBehaviour
{
    public static EnemyReturn Instance { get; private set; }
    private ObjectPool _enemyPool;

    
    void Start()
    {
        _enemyPool = FindAnyObjectByType<ObjectPool>();
    }

    public void OnDisable()
    {
        if (_enemyPool != null)
        {
           
            EnemyManager enemyManager = GetComponent<EnemyManager>();
        }
    }
}
