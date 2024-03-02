using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;
    // Start is called before the first frame update
    void Start()
    {
        // if the audiosource property is empty, assume it is on the same object as the script
        if(audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    public void PlayAudio()
    {
        if (audioSource.isPlaying == false)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}
