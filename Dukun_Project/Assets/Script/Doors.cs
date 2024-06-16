using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public AudioSource doorSound;
    public AudioSource lockedSound; // AudioSource untuk suara pintu terkunci

    public bool inReach;
    public bool isLocked; // Public variable to track if the door is locked
    private bool hasOpenedOnce; // Flag to check if the door has been opened for the first time

    void Start()
    {
        inReach = false;
        hasOpenedOnce = false; // Initialize the flag to false
        // isLocked can now be set from the Unity Inspector, so no need to initialize it here
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (!isLocked) // Check if the door is not locked
            {
                DoorOpens();
            }
            else
            {
                // Play the locked door sound
                lockedSound.Play();
                // You can add a sound or UI message indicating the door is locked
                Debug.Log("The door is locked!");
            }
        }
        else
        {
            DoorCloses();
        }
    }

    void DoorOpens()
    {
        if (!hasOpenedOnce)
        {
            Debug.Log("Berhasil membuka pintu"); // Log message when the door is opened for the first time
            hasOpenedOnce = true; // Set the flag to true after the first open
        }

        door.SetBool("open", true);
        door.SetBool("closed", false);
        doorSound.Play();
    }

    void DoorCloses()
    {
        door.SetBool("open", false);
        door.SetBool("closed", true);
    }

    public void LockDoor()
    {
        isLocked = true;
    }

    public void UnlockDoor()
    {
        isLocked = false;
    }
}
