using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Проверяем имя объекта, с которым столкнулись
        if (other.name == "PlayerCar" || other.name == "base_car" || other.name.Contains("Player"))
        {
            Debug.Log("СТОЛКНОВЕНИЕ! " + other.name);
            
            GameOverManager gom = FindObjectOfType<GameOverManager>();
            if (gom != null)
            {
                gom.TriggerGameOver();
            }
            else
            {
                Debug.LogError("GameOverManager не найден в сцене!");
                Time.timeScale = 0f;
            }
        }
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "PlayerCar" || 
            collision.gameObject.name == "base_car" || 
            collision.gameObject.name.Contains("Player"))
        {
            Debug.Log("КОЛЛИЗИЯ! " + collision.gameObject.name);
            
            GameOverManager gom = FindObjectOfType<GameOverManager>();
            if (gom != null)
            {
                gom.TriggerGameOver();
            }
            else
            {
                Time.timeScale = 0f;
            }
        }
    }
}