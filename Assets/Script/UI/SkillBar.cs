using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
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
