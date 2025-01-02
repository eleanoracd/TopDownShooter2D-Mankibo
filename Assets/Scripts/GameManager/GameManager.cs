using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GameOverManager gameOverManager;

    private void Start()
    {
        gameOverManager = FindObjectOfType<GameOverManager>();

        if (gameOverManager == null)
        {
            Debug.LogError("GameOverManager not found in the scene!");
        }
    }

    public void RestartGame()
    {
        if (gameOverManager != null && gameOverManager.IsGameOver())
        {
            Debug.Log("Restarting game...");
            DestroyAllEnemies();

            Time.timeScale = 1f; // Resume the game
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        else
        {
            Debug.LogError("RestartGame called while the game is not over.");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }

    private void DestroyAllEnemies()
    {
        var enemies = FindObjectsOfType<EnemyLogic>();
        foreach (var enemy in enemies)
        {
            Destroy(enemy.gameObject);
        }
    }
}
