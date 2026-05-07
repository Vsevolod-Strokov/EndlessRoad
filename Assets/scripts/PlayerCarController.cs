using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Скорость")]
    public float startSpeed = 0.01f;        // начальная скорость
    public float maxSpeed = 40f;          // максимальная скорость
    public float speedIncrease = 0.5f;    // прирост скорости в секунду
    
    [Header("Управление")]
    public float steerSpeed = 3f;
    public float maxSteerAngle = 35f;
    public float roadWidth = 2.5f;
    public float tiltAngle = 15f;
    public float smoothTilt = 5f;
    
    private float currentForwardSpeed;
    private float currentSteerAngle = 0f;
    private float currentTiltZ = 0f;
    private float fixedY;
    private float currentYAngle = 0f;
    
    void Start()
    {
        fixedY = transform.position.y;
        currentYAngle = transform.eulerAngles.y;
        currentForwardSpeed = startSpeed;
    }
    
    void Update()
    {
        // === УВЕЛИЧЕНИЕ СКОРОСТИ СО ВРЕМЕНЕМ ===
        currentForwardSpeed += speedIncrease * Time.deltaTime;
        currentForwardSpeed = Mathf.Min(currentForwardSpeed, maxSpeed);
        
        // === УПРАВЛЕНИЕ ===
        float steerInput = 0f;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            steerInput = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            steerInput = 1f;
        
        float targetSteerAngle = steerInput * maxSteerAngle;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, steerSpeed * Time.deltaTime);
        
        currentYAngle += currentSteerAngle * Time.deltaTime;
        
        // === ДВИЖЕНИЕ ===
        Vector3 forward = new Vector3(Mathf.Sin(currentYAngle * Mathf.Deg2Rad), 0, Mathf.Cos(currentYAngle * Mathf.Deg2Rad));
        transform.position += forward * currentForwardSpeed * Time.deltaTime;
        
        // Фиксируем высоту
        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
        
        // Ограничение дорогой
        float clampedX = Mathf.Clamp(transform.position.x, -roadWidth, roadWidth);
        if (transform.position.x != clampedX)
        {
            transform.position = new Vector3(clampedX, fixedY, transform.position.z);
            currentSteerAngle = 0f;
        }
        
        // === НАКЛОН ===
        float targetTiltZ = -steerInput * tiltAngle;
        currentTiltZ = Mathf.Lerp(currentTiltZ, targetTiltZ, smoothTilt * Time.deltaTime);
        
        transform.rotation = Quaternion.Euler(0f, currentYAngle, currentTiltZ);
    }
    
    // Получить текущую скорость (для других скриптов)
    public float GetCurrentSpeed()
    {
        return currentForwardSpeed;
    }
}