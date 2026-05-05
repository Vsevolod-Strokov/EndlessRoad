using UnityEngine;

public class EnemyCarMover : MonoBehaviour
{
    public float speed = 12f;               // обычная скорость
    public float detectionRange = 8f;       // дистанция обнаружения впереди
    public float slowSpeed = 5f;            // скорость при торможении
    
    private float currentSpeed;
    private Transform playerCar;
    
    void Start()
    {
        currentSpeed = speed;
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
            playerCar = player.transform;
    }
    
    void Update()
    {
        // === ПРОВЕРКА ВПЕРЕДИ ===
        bool carAhead = false;
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, detectionRange))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                carAhead = true;
            }
        }
        
        // === СКОРОСТЬ ===
        if (carAhead)
        {
            // Замедляемся
            currentSpeed = Mathf.Lerp(currentSpeed, slowSpeed, Time.deltaTime * 3f);
        }
        else
        {
            // Восстанавливаем скорость
            currentSpeed = Mathf.Lerp(currentSpeed, speed, Time.deltaTime * 2f);
        }
        
        // === ДВИЖЕНИЕ ВПЕРЁД ===
        transform.position += Vector3.forward * currentSpeed * Time.deltaTime;
        
        // === УДАЛЕНИЕ ===
        if (playerCar != null && transform.position.z < playerCar.position.z - 80f)
        {
            Destroy(gameObject);
        }
    }
}