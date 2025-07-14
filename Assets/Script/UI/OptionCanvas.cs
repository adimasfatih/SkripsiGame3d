using UnityEngine;
using UnityEngine.UI;

public class OptionCanvas : MonoBehaviour
{
    public static OptionCanvas Instance;

    public Slider BGMSlider;
    public Slider SFXSlider;
    public Slider MasterSlider;

    public Toggle BGMToggle;
    public Toggle SFXToggle;
    public Toggle MasterToggle;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
}
