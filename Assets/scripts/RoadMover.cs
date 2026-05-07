using UnityEngine;

public class RoadMover : MonoBehaviour
{
    public float speed = 15f;
    public Transform nextRoad;
    public Transform playerCar;

    void Update()
    {
        transform.position += Vector3.back * speed * Time.deltaTime;

        // Если дорога уехала дальше 120м позади игрока
        if (transform.position.z < playerCar.position.z - 100f)
        {
            Vector3 newPos = transform.position;
            newPos.z = nextRoad.position.z + 300f;
            transform.position = newPos;
        }
    }
}