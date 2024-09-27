using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Image healthBarImage; // Reference to the UI Image that represents the health bar

    public void SetHealth(float healthPercentage)
    {
        healthBarImage.fillAmount = healthPercentage; // Update the fill amount of the health bar image
    }
}