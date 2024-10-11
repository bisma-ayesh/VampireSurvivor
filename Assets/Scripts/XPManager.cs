using System;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    private float _currentXP;
    private int _level; 
    public float xpToLevelUp = 10f; 

   
    public float CurrentXP => _currentXP;

    public event Action<float> OnXPChanged; 
    public event Action<int> OnLevelUp; 

   
    public int Level => _level;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; 
        }
        else
        {
            Destroy(gameObject);
        }

       
        SubscribeToExistingEnemies();
    }

    private void SubscribeToExistingEnemies()
    {
        
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (var enemy in enemies)
        {
            
            enemy.OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        }

        Debug.Log($"Found {enemies.Length} enemies in the scene.");
    }

   
    public void HandleEnemyDestroyed(Vector3 enemyPosition, int xpValue)
    {
        AddXP(xpValue); 
    }

    public void AddXP(float xpAmount)
    {
        _currentXP += xpAmount;
        OnXPChanged?.Invoke(_currentXP); 
        CheckLevelUp(); 
    }

    private void CheckLevelUp()
    {
        
        while (_currentXP >= xpToLevelUp) 
        {
            _level++;
            _currentXP -= xpToLevelUp;
            xpToLevelUp += CalculateNextLevelXP(_level); 
            OnLevelUp?.Invoke(_level);
            Debug.Log($"Leveled up! New Level: {_level}");
        }
    }

    private float CalculateNextLevelXP(int currentLevel)
    {
        return 2f * currentLevel; 
    }
}




