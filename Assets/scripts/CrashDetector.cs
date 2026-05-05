using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Если ЭТО враг, и он столкнулся с игроком
        if (other.gameObject.name == "PlayerCar" || 
            other.gameObject.name == "base_car" || 
            other.gameObject.name == "mark2_base")
        {
            Debug.Log("Игра окончена! Столкновение!");
            Time.timeScale = 0f;
        }
    }
}