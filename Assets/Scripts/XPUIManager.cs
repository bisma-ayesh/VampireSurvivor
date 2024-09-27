using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPUIManager : MonoBehaviour
{
    [SerializeField] private Image image;  // XP bar image

    private void Start()
    {
        // Subscribe to XP and level-up events
        XPManager.Instance.OnXPChanged += UpdateXPUI;
        XPManager.Instance.OnLevelUp += HandleLevelUp;

        // Initialize UI with current values
        UpdateXPUI(XPManager.Instance.CurrentXP);
    }

    private void OnDestroy()
    {
        // Unsubscribe when the object is destroyed to avoid memory leaks
        XPManager.Instance.OnXPChanged -= UpdateXPUI;
        XPManager.Instance.OnLevelUp -= HandleLevelUp;
    }

    // Update XP bar based on current XP
    private void UpdateXPUI(float currentXP)
    {
        float xpToLevelUp = XPManager.Instance.xpToLevelUp;
        float normalizedXP = currentXP / xpToLevelUp;
        image.fillAmount = normalizedXP;
    }

    // Handle level up events
    private void HandleLevelUp(int newLevel)
    {
        // Optionally, you can handle additional UI updates here if needed
    }
}
