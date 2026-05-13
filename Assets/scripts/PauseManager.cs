using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public GameObject pausePanel;
    
    private bool isPaused = false;
    
    void Start()
    {
        if (pausePanel != null)
            pausePanel.SetActive(false);
    }
    
    void Update()
    {
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