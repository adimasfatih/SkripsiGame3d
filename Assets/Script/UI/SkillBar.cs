using UnityEngine;
using UnityEngine.UI;

public class SkillBar : MonoBehaviour
{
    public static SkillBar Instance;

    public Slider slider;

    private void Awake()
    {
        Instance = this;
    }


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
