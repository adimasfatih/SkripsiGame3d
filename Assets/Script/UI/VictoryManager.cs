using UnityEngine;

public class VictoryManager : MonoBehaviour
{
    public static VictoryManager Instance;

    public GameObject victoryCanvas;
    public int totalEnemies = 0;
    public int defeatedEnemies = 0;

    void Awake()
    {
        Instance = this;

        if (victoryCanvas != null)
            victoryCanvas.SetActive(false);
    }

    public void RegisterEnemy()
    {
        totalEnemies++;
        Debug.Log("total Enemies = "+ totalEnemies);
    }

    public void EnemyDied()
    {
        defeatedEnemies++;
        Debug.Log("total EnemiesDefeated = " + defeatedEnemies);

        if (defeatedEnemies >= totalEnemies)
        {
            ShowVictory();
            Time.timeScale = 0f;
       
        }
    }

    void ShowVictory()
    {
        if (victoryCanvas != null)
            victoryCanvas.SetActive(true);
    }
}
