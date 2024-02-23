using UnityEngine;

public class ObjectTrigger : MonoBehaviour
{
    public float pushForce = 10f;

    private AudioSource audioSource;
    private GameObject player;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            audioSource.Play();

            Vector3 pushDirection = player.transform.up - player.transform.forward;
            player.GetComponent<Rigidbody>().AddForce(pushDirection * pushForce, ForceMode.Impulse);
        }
    }
}