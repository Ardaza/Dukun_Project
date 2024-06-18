using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objPickup : MonoBehaviour
{
    public GameObject crosshair1, crosshair2;
    public Transform objTransform, cameraTrans;
    public bool interactable, pickedup;
    public Rigidbody objRigidbody;
    public float throwAmount;

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("MainCamera"))
        {
            crosshair1.SetActive(false);
            crosshair2.SetActive(true);
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
                    objTransform.parent = cameraTrans;
                    objRigidbody.useGravity = false;
                    objRigidbody.isKinematic = true; // Set isKinematic to true when picked up
                    pickedup = true;
                }
                else
                {
                    // Drop the object
                    objTransform.parent = null;
                    objRigidbody.useGravity = true;
                    objRigidbody.isKinematic = false; // Set isKinematic to false when dropped
                    pickedup = false;
                }
            }

            if (pickedup && Input.GetMouseButtonDown(1))
            {
                // Throw the object
                objTransform.parent = null;
                objRigidbody.useGravity = true;
                objRigidbody.isKinematic = false; // Ensure isKinematic is false before throwing
                objRigidbody.velocity = cameraTrans.forward * throwAmount * Time.deltaTime;
                pickedup = false;
            }
        }
    }
}
