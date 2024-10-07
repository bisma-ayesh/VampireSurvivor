using System;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    private float currentXP;
    private int level; // Changed to int to represent player levels correctly
    public float xpToLevelUp = 10f; // XP needed to level up

    // Property to access current XP
    public float CurrentXP => currentXP;

    public event Action<float> OnXPChanged; // Triggered when XP changes
    public event Action<int> OnLevelUp; // Changed to Action<int> to match integer level

    // Property to access the player's level
    public int Level => level; // Changed to int

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // Subscribe to enemy destroyed events
        SubscribeToEnemyEvents();
    }

    private void SubscribeToEnemyEvents()
    {
        // Find all enemies in the scene and subscribe to their destruction events
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (var enemy in enemies)
        {
            // Ensure the event subscription matches the method signature
            enemy.OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        }
    }

    private void HandleEnemyDestroyed(Vector3 enemyPosition, int xpValue)
    {
        AddXP(xpValue); // Add XP from the destroyed enemy
    }

    public void AddXP(float xpAmount)
    {
        currentXP += xpAmount;
        Debug.Log($"Gained {xpAmount} XP! Total XP: {currentXP}");
        OnXPChanged?.Invoke(currentXP);
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        // Level up logic
        while (currentXP >= xpToLevelUp) // Use while to handle multiple level-ups in one go
        {
            level++;
            currentXP -= xpToLevelUp; // Reset XP for next level
            xpToLevelUp += CalculateNextLevelXP(level); // Increment XP needed for next level based on function
            Debug.Log($"Leveled up! New Level: {level}");
            OnLevelUp?.Invoke(level);
        }
    }

    private float CalculateNextLevelXP(int currentLevel)
    {
        // Example scaling function for XP needed to level up
        return 2f * currentLevel; // You can adjust this formula based on your desired leveling curve
    }
}




