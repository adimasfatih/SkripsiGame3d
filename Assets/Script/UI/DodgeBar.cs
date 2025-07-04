using UnityEngine;
using UnityEngine.UI;
public class DodgeBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxCooldown(float max)
    {
        slider.maxValue = max;
        slider.value = max;
    }

    public void SetCooldown(float value)
    {
        slider.value = value;
    }

}
