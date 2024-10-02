using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XPUIManager : MonoBehaviour
{
    [SerializeField] private Image image;  

    private void Start()
    {
       
        XPManager.Instance.OnXPChanged += UpdateXPUI;
        XPManager.Instance.OnLevelUp += HandleLevelUp;

  
        UpdateXPUI(XPManager.Instance.CurrentXP);
    }

    private void OnDestroy()
    {
     
        XPManager.Instance.OnXPChanged -= UpdateXPUI;
        XPManager.Instance.OnLevelUp -= HandleLevelUp;
    }

 
    private void UpdateXPUI(float currentXP)
    {
        float xpToLevelUp = XPManager.Instance.xpToLevelUp;
        float normalizedXP = currentXP / xpToLevelUp;
        image.fillAmount = normalizedXP;
    }

  
    private void HandleLevelUp(int newLevel)
    {
       
    }
}
