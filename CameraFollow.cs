using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private float fixedY;
    private float fixedZ;

    void Start()
    {
        // Guardamos la posici�n Y y Z actuales de la c�mara como fijas
        fixedY = transform.position.y;
        fixedZ = transform.position.z;
    }

    void LateUpdate()
    {
        if (!target) return;

        // Calcula la nueva posici�n solo en el eje X
        float desiredX = target.position.x + offset.x;
        float smoothedX = Mathf.Lerp(transform.position.x, desiredX, smoothSpeed);

        // Asigna la nueva posici�n, manteniendo Y y Z fijos
        transform.position = new Vector3(smoothedX, fixedY + offset.y, fixedZ + offset.z);
    }
}
