using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // Subscribe to scene load events
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // First scene load
        HandleMusicForScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleMusicForScene(int sceneIndex)
    {
        if (sceneIndex == 0)
        {
            SoundManager.Instance?.PlayMainMenuMusic();
        }
        else
        {
            SoundManager.Instance.lastCombatIndex = -1; // Optional reset
            SoundManager.Instance?.PlayCombatMusic();
        }
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleMusicForScene(scene.buildIndex);
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextScene < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextScene);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadMainMenu()
    {
        Destroy(SoundManager.Instance?.gameObject);
        Destroy(gameObject);
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
