using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        if (cam != null)
            transform.position = cam.position + new Vector3(0, 0, 5); // Asegura que quede visible
    }
}
