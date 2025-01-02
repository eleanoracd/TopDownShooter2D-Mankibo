using TMPro;
using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    [SerializeField] private PlayerLogic playerLogic;
    [SerializeField] private SpawnManager spawnManager;
    [SerializeField] private TextMeshProUGUI gameOverText;
    [SerializeField] private GameObject gameOverUI;

    private bool isGameOver = false;

    private void Start()
    {
        if (spawnManager == null)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }

        Time.timeScale = 1f; // Ensure normal game time on scene load
        isGameOver = false;
        gameOverUI.SetActive(false);
    }

    private void Update()
    {
        if (isGameOver) return;

        if (playerLogic != null && playerLogic.GetCurrentHealth() <= 0)
        {
            ActivateGameOverUI("You died.");
        }
        else if (spawnManager != null && spawnManager.IsWaveOver() && !AnyEnemiesRemaining() && playerLogic.GetCurrentHealth() > 0)
        {
            ActivateGameOverUI("You survived.");
        }
    }

    private void ActivateGameOverUI(string message)
    {
        isGameOver = true;
        gameOverText.text = message;
        gameOverUI.SetActive(true);
        Time.timeScale = 0f; // Pause the game
    }

    private bool AnyEnemiesRemaining()
    {
        return FindObjectsOfType<EnemyLogic>().Length > 0;
    }

    public bool IsGameOver()
    {
        return isGameOver;
    }
}
