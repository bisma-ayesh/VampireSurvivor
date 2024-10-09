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
            // Get the EnemyManager component to access the xpValue
            EnemyManager enemyManager = GetComponent<EnemyManager>();

            if (enemyManager != null)
            {
                // Pass the xpValue when returning the enemy to the pool
                _enemyPool.ReturnEnemy(this.gameObject, enemyManager.XPValue);
            }
            else
            {
                Debug.LogWarning("EnemyManager component not found on this GameObject.");
            }
        }
    }
}
