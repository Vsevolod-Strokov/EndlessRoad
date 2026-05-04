using UnityEngine;

public class PlayerCarController : MonoBehaviour
{
    [Header("Настройки движения")]
    public float forwardSpeed = 15f;
    public float steerSpeed = 3f;
    public float maxSteerAngle = 35f;
    public float roadWidth = 2.5f;
    public float tiltAngle = 15f;
    public float smoothTilt = 5f;
    
    private float currentSteerAngle = 0f;
    private float currentTiltZ = 0f;
    private float fixedY;
    
    void Start()
    {
        fixedY = transform.position.y;
    }
    
    void Update()
    {
        float steerInput = 0f;
        
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            steerInput = -1f;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            steerInput = 1f;
        
        float targetSteerAngle = steerInput * maxSteerAngle;
        currentSteerAngle = Mathf.Lerp(currentSteerAngle, targetSteerAngle, steerSpeed * Time.deltaTime);
        
        transform.Rotate(0f, currentSteerAngle * Time.deltaTime, 0f);
        
        Vector3 forwardMove = transform.forward * forwardSpeed * Time.deltaTime;
        forwardMove.y = 0f;
        transform.position += forwardMove;
        
        transform.position = new Vector3(transform.position.x, fixedY, transform.position.z);
        
        float clampedX = Mathf.Clamp(transform.position.x, -roadWidth, roadWidth);
        if (transform.position.x != clampedX)
        {
            transform.position = new Vector3(clampedX, fixedY, transform.position.z);
            currentSteerAngle = 0f;
        }
        
        float targetTiltZ = -steerInput * tiltAngle;
        currentTiltZ = Mathf.Lerp(currentTiltZ, targetTiltZ, smoothTilt * Time.deltaTime);
        
        Vector3 currentEuler = transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(0f, currentEuler.y, currentTiltZ);
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "LeftWall" || other.gameObject.name == "RightWall")
        {
            GameOver();
        }
    }
    
    void GameOver()
    {
        Debug.Log("Игра окончена! Врезался в забор!");
        Time.timeScale = 0f;
    }
}