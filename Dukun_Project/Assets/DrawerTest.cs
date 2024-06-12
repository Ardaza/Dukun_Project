using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawerTest : MonoBehaviour
{
    public GameObject openText;
    public AudioSource doorSound;

    public bool inReach;

    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float doorMoveSpeed = 2f;  // Speed at which the door moves

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

        // Smoothly move the door to its target position
        Vector3 targetPosition = doorIsOpen ? openPosition : closedPosition;
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * doorMoveSpeed);
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
