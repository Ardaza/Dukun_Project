using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Add this namespace to use TextMeshPro

public class Doors : MonoBehaviour
{
    public Animator door;
    public GameObject Interact;
    public GameObject openText; // Assuming this is your TextMeshPro UI element
    public TextMeshProUGUI openTextTMP; // Add this line to reference your TextMeshPro component
    public AudioSource doorSound;
    public AudioSource lockedSound;
    public GameObject lockedText;

    public bool inReach;
    public bool isLocked;
    private bool hasOpenedOnce;

    void Start()
    {
        inReach = false;
        hasOpenedOnce = false;
        openText.SetActive(false); // Set openText to be inactive at the start of the game
        openTextTMP = openText.GetComponent<TextMeshProUGUI>();
        Interact.SetActive(false);
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            openText.SetActive(true);
            Interact.SetActive(true);
            openTextTMP.text = gameObject.name; // Set the text to the name of the GameObject
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            Interact.SetActive(false);
            openText.SetActive(false);
            lockedText.SetActive(false);
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
                Interact.SetActive(false);
                openText.SetActive(false);
                lockedText.SetActive(true);
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