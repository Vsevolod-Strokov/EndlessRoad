using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    [Header("Префабы врагов")]
    public GameObject[] enemyPrefabs;
    
    [Header("Цель")]
    public Transform playerCar;
    
    [Header("Настройки спавна")]
    public float spawnDistance = 120f;
    public float startSpawnRate = 2f;     // начальный интервал
    public float minSpawnRate = 0.3f;      // минимальный интервал (сложнее)
    public float spawnRateDecrease = 0.05f; // уменьшение интервала в секунду
    public float startMinSpeed = 8f;       // нач. мин. скорость врагов
    public float startMaxSpeed = 14f;      // нач. макс. скорость врагов
    public float speedIncrease = 0.3f;     // прирост скорости врагов в секунду
    
    [Header("Полосы")]
    public float roadWidth = 3f;
    public int laneCount = 4;
    
    private float nextSpawnTime;
    private int lastLane = -1;
    private float currentMinSpawnRate;
    private float currentMaxSpawnRate;
    private float currentMinSpeed;
    private float currentMaxSpeed;
    private float gameTime;
    
    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(1f, 3f);
        currentMinSpawnRate = startSpawnRate - 0.5f;
        currentMaxSpawnRate = startSpawnRate + 0.5f;
        currentMinSpeed = startMinSpeed;
        currentMaxSpeed = startMaxSpeed;
    }
    
    void Update()
    {
        gameTime += Time.deltaTime;
        
        // === УВЕЛИЧЕНИЕ СЛОЖНОСТИ ===
        // Уменьшаем интервал между спавнами
        currentMinSpawnRate = Mathf.Max(minSpawnRate, startSpawnRate - 0.5f - gameTime * spawnRateDecrease);
        currentMaxSpawnRate = Mathf.Max(minSpawnRate + 0.3f, startSpawnRate + 0.5f - gameTime * spawnRateDecrease);
        
        // Увеличиваем скорость врагов
        currentMinSpeed = startMinSpeed;
        currentMaxSpeed = startMaxSpeed;
        
        // === СПАВН ===
        if (Time.time >= nextSpawnTime)
        {
            SpawnCar();
            nextSpawnTime = Time.time + Random.Range(currentMinSpawnRate, currentMaxSpawnRate);
        }
    }
    
    void SpawnCar()
    {
        if (enemyPrefabs.Length == 0 || playerCar == null) return;
        
        int lane;
        do
        {
            lane = Random.Range(0, laneCount);
        } while (lane == lastLane && laneCount > 1);
        lastLane = lane;
        
        float laneWidth = (roadWidth * 2f) / laneCount;
        float xPos = -roadWidth + lane * laneWidth + laneWidth / 2f;
        xPos += Random.Range(-0.4f, 0.4f);
        
        Vector3 spawnPos = new Vector3(
            xPos,
            playerCar.position.y + 0.9f,
            playerCar.position.z + spawnDistance
        );
        
        Quaternion rotation = Quaternion.Euler(0f, 90f, 0f);
        
        GameObject prefab = enemyPrefabs[Random.Range(0, enemyPrefabs.Length)];
        GameObject enemy = Instantiate(prefab, spawnPos, rotation);
        
        EnemyCarMover mover = enemy.GetComponent<EnemyCarMover>();
        if (mover != null)
        {
            mover.speed = Random.Range(currentMinSpeed, currentMaxSpeed);
        }
    }
}