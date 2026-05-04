using UnityEngine;

public class CrashDetector : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        // Проверяем, что столкнулись именно с моделью машины
        if (other.gameObject.name == "base_car" || other.gameObject.name == "mark2_base")
        {
            Debug.Log("Игра окончена! Врезался в забор!");
            Time.timeScale = 0f;
        }
    }
}