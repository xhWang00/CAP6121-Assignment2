using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5f;
    public GameObject footstepSound;

    void Start()
    {

    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 movement = (cameraForward * verticalInput) + (Camera.main.transform.right * horizontalInput);
        movement.Normalize();

        transform.Translate(movement * speed * Time.deltaTime);

        if (movement != Vector3.zero && !footstepSound.GetComponent<AudioSource>().isPlaying)
        {
            footstepSound.GetComponent<AudioSource>().Play();
        }
        else if (movement == Vector3.zero && footstepSound.GetComponent<AudioSource>().isPlaying)
        {
            footstepSound.GetComponent<AudioSource>().Stop();
        }

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 5f, 0), ForceMode.Impulse);
        }
    }
}
