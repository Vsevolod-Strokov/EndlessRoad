using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel;
    public TextMeshProUGUI finalScoreText;
    public ScoreManager scoreManager;
    
    void Start()
    {
        gameOverPanel.SetActive(false);
    }
    
    public void TriggerGameOver()
    {
        gameOverPanel.SetActive(true);
        
        if (scoreManager != null && finalScoreText != null)
        {
            float score = scoreManager.GetScore();
            finalScoreText.text = "SCORE: " + Mathf.FloorToInt(score).ToString("N0");
        }
        
        // Останавливаем время
        Time.timeScale = 0f;
    }
    
    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("endlessroad");
    }
    
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}