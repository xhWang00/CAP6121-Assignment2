using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Rotation speed in degrees per second
    private float rotationSpeed = 30f * 360f / 60f;

    void Update()
    {
        // Calculate rotation for this frame
        float rotationAmount = rotationSpeed * Time.deltaTime;

        // Apply rotation
        transform.Rotate(0, 0, rotationAmount);
    }
}