using System;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    private float currentXP;
    private int level; // Represents player levels
    public float xpToLevelUp = 10f; // XP needed to level up

    // Property to access current XP
    public float CurrentXP => currentXP;

    public event Action<float> OnXPChanged; // Triggered when XP changes
    public event Action<int> OnLevelUp; // Triggered when the player levels up

    // Property to access the player's level
    public int Level => level;

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


        // Subscribe to enemy destroyed events already in the scene
        SubscribeToExistingEnemies();
    }

    private void SubscribeToExistingEnemies()
    {
        // Find all enemies in the scene and subscribe to their destruction events
        EnemyManager[] enemies = FindObjectsOfType<EnemyManager>();
        foreach (var enemy in enemies)
        {
            // Ensure the event subscription matches the method signature
            enemy.OnEnemyDestroyed.AddListener(HandleEnemyDestroyed);
        }

        Debug.Log($"Found {enemies.Length} enemies in the scene.");

    }

    // This method can now be called by any enemy to handle its destruction
    public void HandleEnemyDestroyed(Vector3 enemyPosition, int xpValue)
    {
        AddXP(xpValue); // Add XP from the destroyed enemy
    }

    public void AddXP(float xpAmount)
    {
        currentXP += xpAmount;
        //Debug.Log($"Gained {xpAmount} XP! Total XP: {currentXP}");
        OnXPChanged?.Invoke(currentXP); // Notify listeners about the XP change
        CheckLevelUp(); // Check if the player leveled up
    }

    private void CheckLevelUp()
    {
        // Level up logic
        while (currentXP >= xpToLevelUp) // Handle multiple level-ups if XP exceeds the threshold
        {
            level++;
            currentXP -= xpToLevelUp; // Reset XP for next level
            xpToLevelUp += CalculateNextLevelXP(level); // Calculate XP required for the next level
           // Debug.Log($"Leveled up! New Level: {level}");
            OnLevelUp?.Invoke(level);// Notify listeners about the level up
            Debug.Log(OnLevelUp + "invoked");
        }
    }

    private float CalculateNextLevelXP(int currentLevel)
    {
        // Example scaling function for XP needed to level up
        return 2f * currentLevel; // You can adjust this formula for your desired leveling curve
    }
}





