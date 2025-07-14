using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseCanvas;
    public GameObject OptionCanvas;
    private bool isPaused = false;

    void Start()
    {
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (pauseCanvas != null)
            pauseCanvas.SetActive(isPaused);

        Time.timeScale = isPaused ? 0f : 1f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
        if (pauseCanvas != null)
            pauseCanvas.SetActive(false);
    }

    public void OptionMenu()
    {
        OptionCanvas.SetActive(true);
        pauseCanvas.SetActive(false);
    }

    public void BackToPause()
    {
        OptionCanvas.SetActive(false);
        pauseCanvas.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        GameManager.Instance.LoadMainMenu(); // assuming scene 0 = main menu
    }

    public void NextLevel()
    {
        GameManager.Instance.LoadNextLevel(); // assuming scene 0 = main menu
    }
}
