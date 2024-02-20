using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Get input from the controller
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Get the camera's forward vector without vertical component
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        // Calculate movement direction based on camera direction
        Vector3 movement = (cameraForward * verticalInput) + (Camera.main.transform.right * horizontalInput);
        movement.Normalize();

        // Move the player
        transform.Translate(movement * speed * Time.deltaTime);
    }
}