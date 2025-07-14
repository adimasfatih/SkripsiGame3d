using UnityEngine;

public class MainMenuUI : MonoBehaviour
{
    public GameObject mainMenuCanvas;
    public GameObject optionCanvas;
   

    void Start()
    {
        // Make sure Main Menu is active on start
        mainMenuCanvas.SetActive(true);
        optionCanvas.SetActive(false);
     
    }

    public void OnStartButton()
    {
        // Load next scene (assuming Scene 1 is the first level)
        GameManager.Instance.LoadNextLevel();
    }

    public void OnOptionsButton()
    {
        mainMenuCanvas.SetActive(false);
        optionCanvas.SetActive(true);
    }

    public void OnBackToMainMenuButton()
    {
        optionCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);
    }

    public void OnExitButton()
    {
        Debug.Log("Exiting game...");
        Application.Quit();

        // Will not quit in editor
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
