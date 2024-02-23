using UnityEngine;

public class EatCake : MonoBehaviour
{
    public AudioClip clip;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("MainCamera"))
        {
            GameObject audioPlayer = GameObject.FindWithTag("AudioPlayer");
            if (audioPlayer != null)
            {
                AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();
                }
                else
                {
                    Debug.LogError("No AudioSource component found on the AudioPlayer object.");
                }
            }
            else
            {
                Debug.LogError("No object with the tag AudioPlayer found.");
            }

            Destroy(gameObject);
        }
    }
}
