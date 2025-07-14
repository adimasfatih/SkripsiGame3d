using UnityEngine;

public class UIManager : MonoBehaviour
{
  
    void Awake()
    {
        SoundManager.Instance.SetupOnLoad();
    }

}
