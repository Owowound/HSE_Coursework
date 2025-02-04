using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    public float cameraSpeed;

    public Vector3 offset;

    void FixedUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, target.position.y * 0.5f, transform.position.z) + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
    }
}