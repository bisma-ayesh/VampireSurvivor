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
      

  
        UpdateXPUI(XPManager.Instance.CurrentXP);
    }

    private void OnDestroy()
    {
     
        XPManager.Instance.OnXPChanged -= UpdateXPUI;
      
    }

 
    private void UpdateXPUI(float currentXP)
    {
       
            float xpToLevelUp = XPManager.Instance.xpToLevelUp;
            float normalizedXP = currentXP / xpToLevelUp;
            image.fillAmount = normalizedXP;
        
    }
   

}
