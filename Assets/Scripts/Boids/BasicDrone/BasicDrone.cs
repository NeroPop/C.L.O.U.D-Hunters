using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDrone : MonoBehaviour
{
    private Rigidbody rigidbody;
    private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips;
    [SerializeField] private AudioClip deathClip;
    private float timer;
    private float timeUntilNextVoiceLine;
    private bool dead;
    [SerializeField] private bool playAudio;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        timeUntilNextVoiceLine = Random.Range(0, 10);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead)
        {
            timer += Time.deltaTime;

            if(timer >= timeUntilNextVoiceLine && playAudio)
            {
                audioSource.clip = audioClips[Random.Range(0, audioClips.Length)];
                audioSource.Play();
                timeUntilNextVoiceLine = Random.Range(0, 10);
                timer = 0.0f;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "disk")
        {
            dead = true;
            rigidbody.isKinematic = false;
            audioSource.clip = deathClip;
            audioSource.Play();
        }
    }
}
