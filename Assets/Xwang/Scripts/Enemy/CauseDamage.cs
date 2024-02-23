using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip clip;

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "MainCamera")
        {
            GameObject audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer");
            if (audioPlayer != null)
            {
                AudioSource audioSource = audioPlayer.GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    audioSource.clip = clip;
                    audioSource.Play();

                    Destroy(this.gameObject);
                }
                else
                {
                    Debug.LogError("No AudioSource component found on the AudioPlayer object.");
                }
            }
            else
            {
                Debug.LogError("No object found with the tag AudioPlayer.");
            }
        }
    }
}