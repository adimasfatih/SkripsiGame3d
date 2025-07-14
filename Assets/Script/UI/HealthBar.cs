using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class HealthBar : MonoBehaviour
{
    public static HealthBar Instance;
    public Slider slider;

    private void Awake()
    {
        Instance = this;
    }

    
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }

    public void SetHealth(int health)
    {
        slider.value = health;
    }
   
}
