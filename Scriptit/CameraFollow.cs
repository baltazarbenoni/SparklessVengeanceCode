using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float smoothSpeed = 0.125f; // The speed with which the camera follows the target
    public Vector3 offset; // The offset from the target's position

    void LateUpdate()
    {
        // Check if the target exists
        if (target != null)
        {
            // Calculate the desired position for the camera
            Vector3 desiredPosition = target.position + offset;

            // Optionally, reset the desired position's z-value to the camera's original z-value
            desiredPosition.z = transform.position.z;

            // Smoothly move the camera towards the desired position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

            // Set the position of the camera
            transform.position = smoothedPosition;
        }
    }
}
