using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset = new Vector3(0, 3, -8);
    public float lookSideAmount = 2f;  // насколько камера смотрит в сторону поворота

    void LateUpdate()
    {
        if (target == null) return;

        // Позиция — строго сзади сверху
        transform.position = target.position + offset;

        // Смотрим на машину + немного в сторону её поворота
        Vector3 lookPoint = target.position + target.forward * 5f;
        transform.LookAt(lookPoint);
        
        // Доворачиваем камеру в сторону поворота машины
        float carYRotation = target.rotation.eulerAngles.y;
        if (carYRotation > 180) carYRotation -= 360; // переводим в диапазон -180..180
        
        transform.RotateAround(target.position, Vector3.up, carYRotation * lookSideAmount * 0.1f);
    }
}