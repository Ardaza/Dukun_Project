using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    public AudioClip[] audioClips; // Array of audio clips to play randomly
    public float minTimeBetweenSounds = 1f; // Minimum time between sounds
    public float maxTimeBetweenSounds = 5f; // Maximum time between sounds

    private AudioSource audioSource; // Reference to the AudioSource component
    private float timeToNextSound; // Time until the next sound plays
    private bool isSoundPlaying; // Check if sound is playing

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        ScheduleNextSound();
    }

    void Update()
    {
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