using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    public Doors doorScript; // Reference to the door script
    public GameObject pickupText; // Text to display when in range to pick up the key
    public AudioSource pickupSound; // Reference to the audio source for the pickup sound

    private bool inReach;

    void Start()
    {
        inReach = false;
        pickupText.SetActive(false); // Make sure the text is initially hidden
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickupText.SetActive(true); // Show the text when the player is in range
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickupText.SetActive(false); // Hide the text when the player is out of range
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            UnlockDoor();
        }
    }

    void UnlockDoor()
    {
        doorScript.UnlockDoor(); // Call the method to unlock the door
        pickupText.SetActive(false); // Hide the text
        pickupSound.Play(); // Play the pickup sound
        Destroy(gameObject, pickupSound.clip.length); // Destroy the key object after the sound has finished playing
    }
}
