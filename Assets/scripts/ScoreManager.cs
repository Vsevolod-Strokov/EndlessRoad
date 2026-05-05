using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public float scorePerSecond = 10f;
    
    private float score = 0f;
    private float displayScore = 0f;
    
    void Update()
    {
        score += scorePerSecond * Time.deltaTime;
        displayScore = Mathf.Lerp(displayScore, score, 10f * Time.deltaTime);
        scoreText.text = Mathf.FloorToInt(displayScore).ToString("N0");
    }
}