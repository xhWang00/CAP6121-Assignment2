using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject footstepSound; // Reference to the footstep sound object

    void Start()
    {

    }

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

        // If the player is moving and the footstep sound is not already playing, play it
        if (movement != Vector3.zero && !footstepSound.GetComponent<AudioSource>().isPlaying)
        {
            footstepSound.GetComponent<AudioSource>().Play();
        }
        // If the player is not moving and the footstep sound is playing, stop it
        else if (movement == Vector3.zero && footstepSound.GetComponent<AudioSource>().isPlaying)
        {
            footstepSound.GetComponent<AudioSource>().Stop();
        }
    }
}
