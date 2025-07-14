using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public static DeathManager Instance;

    public GameObject DeathCanvas;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void PlayerDied()
    {
        if (DeathCanvas != null)
            DeathCanvas.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        GameManager.Instance.RestartLevel();
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.LoadMainMenu(); // assuming scene 0 = main menu
    }
}
