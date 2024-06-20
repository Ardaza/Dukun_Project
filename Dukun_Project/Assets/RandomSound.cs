using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Import the SceneManagement namespace

public class RandomSound : MonoBehaviour
{
    public AudioClip[] audioClips; // Array of audio clips to play randomly
    public float minTimeBetweenSounds = 1f; // Minimum time between sounds
    public float maxTimeBetweenSounds = 5f; // Maximum time between sounds

    private AudioSource audioSource; // Reference to the AudioSource component
    private float timeToNextSound; // Time until the next sound plays

    void Start()
    {
        // Check if the current scene is Scene 4
        if (SceneManager.GetActiveScene().buildIndex == 4)
        {
            audioSource = GetComponent<AudioSource>();
            ScheduleNextSound();
        }
        else
        {
            // Disable the script if not in Scene 4
            this.enabled = false;
        }
    }

    void Update()
    {
        // Only update if the script is enabled (i.e., in Scene 4)
        if (!audioSource.isPlaying)
        {
            timeToNextSound -= Time.deltaTime;

            if (timeToNextSound <= 0f)
            {
                PlayRandomSound();
                ScheduleNextSound();
            }
        }
    }

    void ScheduleNextSound()
    {
        timeToNextSound = Random.Range(minTimeBetweenSounds, maxTimeBetweenSounds);
    }

    void PlayRandomSound()
    {
        if (audioClips.Length == 0)
        {
            Debug.LogWarning("No audio clips assigned to RandomSoundPlayer.");
            return;
        }

        int randomIndex = Random.Range(0, audioClips.Length);
        AudioClip randomClip = audioClips[randomIndex];
        audioSource.clip = randomClip;
        audioSource.Play();
    }
}
