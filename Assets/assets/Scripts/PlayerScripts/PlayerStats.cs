using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    [SerializeField]
    private Image healthStats;
    [SerializeField]
    private Image staminaStats;

    public void DisplayHealthStats(float healthValue)
    {
        healthValue /= 100;

        healthStats.fillAmount = healthValue;
    }

    public void DisplayStaminaStats(float staminaValue)
    {
        staminaValue /= 100;

        staminaStats.fillAmount = staminaValue;
    }

}
