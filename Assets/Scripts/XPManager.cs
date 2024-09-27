using System;
using UnityEngine;

public class XPManager : MonoBehaviour
{
    public static XPManager Instance { get; private set; }

    private int currentXP;
    private int level;
    private int xpToLevelUp = 100; // XP needed to level up

    // Property to access current XP
    public int CurrentXP => currentXP;

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
    }

    public void AddXP(int xpAmount)
    {
        currentXP += xpAmount;
        Debug.Log($"Gained {xpAmount} XP! Total XP: {currentXP}");
        CheckLevelUp();
    }

    private void CheckLevelUp()
    {
        if (currentXP >= xpToLevelUp)
        {
            level++;
            currentXP -= xpToLevelUp; // Reset XP for next level
            xpToLevelUp += 50; // Increment XP needed for next level (optional)
            Debug.Log($"Leveled up! New Level: {level}");
        }
    }
}
