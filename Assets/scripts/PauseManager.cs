using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameOverManager gameOverManager;  // ссылка на GameOverManager
    
    private bool isPaused = false;
    
    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
        
        if (gameOverManager == null)
            gameOverManager = FindObjectOfType<GameOverManager>();
    }
    
    void Update()
    {
        // Не работаем если Game Over активен
        if (gameOverManager != null && gameOverManager.gameOverPanel != null && 
            gameOverManager.gameOverPanel.activeSelf)
            return;
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePause();
        }
    }
    
    void TogglePause()
    {
        isPaused = !isPaused;
        
        if (isPaused)
        {
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }
    }
    
    void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}