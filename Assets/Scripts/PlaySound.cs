using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioClip clip;
    public void Play()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
