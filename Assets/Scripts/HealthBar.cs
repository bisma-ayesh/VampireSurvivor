using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; 

    public void SetHealth(float healthPercentage)
    {
        healthBarImage.fillAmount = healthPercentage; 
    }
}