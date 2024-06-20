using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;
    public Vector3 pickupOffset = new Vector3(0, 0, 2f); // Offset to adjust the position of the object when picked up
    public TextMeshProUGUI interactionText; // TextMeshProUGUI element for interaction text
    public AudioSource pickupAudioSource; // AudioSource component for pickup sound

    void Start()
    {
        // Ensure the AudioSource component is assigned
        if (pickupAudioSource == null)
        {
            pickupAudioSource = GetComponent<AudioSource>();
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
            interactionText.text = "[G] Grab";
            interactionText.gameObject.SetActive(true);
            interactable = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            if (!pickedup)
            {
                crosshair1.SetActive(true);
                crosshair2.SetActive(false);
                interactionText.gameObject.SetActive(false);
                interactable = false;
            }
        }
    }

    void Update()
    {
        if (interactable)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                if (!pickedup)
                {
                    // Pick up the object
                    PickUpObject();
                }
                else
                {
                    // Drop the object
                    DropObject();
                }
            }

            if (pickedup && Input.GetMouseButtonDown(1))
            {
                // Throw the object
                ThrowObject();
            }
        }
    }

    void PickUpObject()
    {
        objTransform.parent = cameraTrans;
        objTransform.localPosition = pickupOffset; // Adjust the position with the offset
        objRigidbody.useGravity = false;
        objRigidbody.isKinematic = true; // Set isKinematic to true when picked up
        interactionText.gameObject.SetActive(false); // Hide interaction text
        pickedup = true;

        // Play the pickup sound
        if (pickupAudioSource != null)
        {
            pickupAudioSource.Play();
        }
    }

    void DropObject()
    {
        objTransform.parent = null;
        objRigidbody.useGravity = true;
        objRigidbody.isKinematic = false; // Set isKinematic to false when dropped
        pickedup = false;
        interactionText.gameObject.SetActive(false); // Hide interaction text
    }

    void ThrowObject()
    {
        objTransform.parent = null;
        objRigidbody.useGravity = true;
        objRigidbody.isKinematic = false; // Ensure isKinematic is false before throwing
        objRigidbody.velocity = cameraTrans.forward * throwAmount;
        pickedup = false;
        interactionText.gameObject.SetActive(false); // Hide interaction text
    }
}
