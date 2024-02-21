using UnityEngine;

public class PlaySoundOnCollision : MonoBehaviour
{
    public AudioClip clip;

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.tag == "Player")
        {
            GameObject audioPlayer = GameObject.FindGameObjectWithTag("AudioPlayer");
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
                Debug.LogError("No object found with the tag AudioPlayer.");
            }
        }
    }
}