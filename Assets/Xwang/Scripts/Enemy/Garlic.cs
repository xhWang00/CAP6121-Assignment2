using System.Collections;
using UnityEngine;

public class AudioOnCollision : MonoBehaviour
{
    public AudioClip clip; // The audio clip that will be played

    void OnCollisionEnter(Collision collision)
    {
        // Check if the game object we collided with has the tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the AudioSource from the enemy
            AudioSource audioSource = collision.gameObject.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                // Play the audio clip
                audioSource.clip = clip;
                audioSource.Play();

                // Start the coroutine to destroy the enemy after 3 seconds
                StartCoroutine(DestroyAfterSeconds(collision.gameObject, 3));
            }
        }
    }

    IEnumerator DestroyAfterSeconds(GameObject obj, float seconds)
    {
        // Wait for the specified number of seconds
        yield return new WaitForSeconds(seconds);

        // Destroy the game object
        Destroy(obj);
    }
}