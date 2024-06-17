using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject openText;
    public AudioSource doorSound;
    public AudioSource lockedSound; // AudioSource untuk suara pintu terkunci
    public TextMeshProUGUI lockedText; // Reference to the TextMeshPro object

    public bool inReach;
    public bool isLocked; // Public variable to track if the door is locked
    private bool hasOpenedOnce; // Flag to check if the door has been opened for the first time

    void Start()
    {
        inReach = false;
        hasOpenedOnce = false; // Initialize the flag to false
        // isLocked can now be set from the Unity Inspector, so no need to initialize it here
        lockedText.gameObject.SetActive(false); // Ensure the locked text is initially hidden
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
                // Display the "locked" message
                StartCoroutine(DisplayLockedMessage());
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

    IEnumerator DisplayLockedMessage()
    {
        lockedText.gameObject.SetActive(true); // Show the locked message
        yield return new WaitForSeconds(1); // Wait for 2 seconds (or however long you want the message to be displayed)
        lockedText.gameObject.SetActive(false); // Hide the locked message
    }
}
