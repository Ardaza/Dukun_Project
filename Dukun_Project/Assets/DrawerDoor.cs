using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerDoor : MonoBehaviour
{
    public GameObject openText;
    public AudioSource doorSound;

    public bool inReach;

    public Vector3 openRotation;  // Target rotation for the open state
    public Vector3 closedRotation;  // Target rotation for the closed state
    public float doorRotateSpeed = 2f;  // Speed at which the door rotates

    private bool doorIsOpen = false;

    void Start()
    {
        inReach = false;
        doorIsOpen = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = true;
            openText.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Reach"))
        {
            inReach = false;
            openText.SetActive(false);
        }
    }

    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact"))
        {
            if (doorIsOpen)
            {
                DoorCloses();
            }
            else
            {
                DoorOpens();
            }
        }

        // Smoothly rotate the door to its target rotation
        Quaternion targetRotation = Quaternion.Euler(doorIsOpen ? openRotation : closedRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * doorRotateSpeed);
    }

    void DoorOpens()
    {
        doorSound.Play();
        doorIsOpen = true;
    }

    void DoorCloses()
    {
        doorSound.Play();
        doorIsOpen = false;
    }
}
