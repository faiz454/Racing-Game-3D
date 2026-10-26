using UnityEngine;

public class CarCameraFollow : MonoBehaviour
{
    public Transform target; // Drag your PlayerCar here
    public Vector3 offset = new Vector3(0f, 3f, -5f);
    public float smoothSpeed = 10f;

    void LateUpdate() // Back to LateUpdate for smooth screen-refresh tracking
    {
        if (target == null) return;

        // Calculate desired position behind the car
        Vector3 desiredPosition = target.position + target.TransformDirection(offset);

        // Smoothly move towards that position using deltaTime
        transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

        // Look at the car smoothly
        Vector3 lookPosition = target.position + Vector3.up * 1.5f;
        Quaternion targetRotation = Quaternion.LookRotation(lookPosition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }
}