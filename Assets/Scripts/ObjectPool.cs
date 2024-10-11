using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
   
    private Dictionary<string, Queue<GameObject>> enemyPool = new Dictionary<string, Queue<GameObject>>();

    
    private XPManager _xpManager;

    private void Awake()
    {
        
        _xpManager = XPManager.Instance; 
    }

    public GameObject GetEnemy(GameObject enemyPrefab)
    {
        
        if (enemyPool.TryGetValue(enemyPrefab.name, out Queue<GameObject> enemyPoolList))
        {
        
            if (enemyPoolList.Count == 0)
            {
                return CreateNewEnemy(enemyPrefab);
            }
            else
            {
                
                GameObject _enemy = enemyPoolList.Dequeue();
                _enemy.SetActive(true);
                return _enemy;
            }
        }
        else
        {
          
            return CreateNewEnemy(enemyPrefab);
        }
    }

    private GameObject CreateNewEnemy(GameObject _enemyPrefab)
    {
        GameObject instantiatedEnemy = Instantiate(_enemyPrefab);
        instantiatedEnemy.name = _enemyPrefab.name; 
        return instantiatedEnemy; 
    }

    
    public void ReturnEnemy(GameObject enemyInstance, int xpValue) 
    {
       
        if (enemyPool.TryGetValue(enemyInstance.name, out Queue<GameObject> enemyPoolList))
        {
            
            enemyPoolList.Enqueue(enemyInstance);
        }
        else
        {
          
            Queue<GameObject> newPool = new Queue<GameObject>();
            newPool.Enqueue(enemyInstance);
            enemyPool.Add(enemyInstance.name, newPool); 
        }

        
        enemyInstance.SetActive(false);

      
        if (_xpManager != null)
        {
            EnemyManager enemyManager = enemyInstance.GetComponent<EnemyManager>();
            if (enemyManager != null)
            {
                _xpManager.HandleEnemyDestroyed(enemyInstance.transform.position, enemyManager.XPValue);
            }
        }
    }
}



