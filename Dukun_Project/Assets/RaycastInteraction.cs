using UnityEngine;

public class RaycastInteraction : MonoBehaviour
{
    public float interactionDistance = 2f; // Jarak interaksi maksimal
    public LayerMask interactionLayer; // Layer untuk objek yang bisa diinteraksi
    public string interactionTag = "reach"; // Tag untuk objek yang bisa diinteraksi

    void Update()
    {
        if (Input.GetButtonDown("Interact")) // Atur input sesuai kebutuhan (misalnya, tombol "E")
        {
            PerformRaycast();
        }
    }

    void PerformRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionDistance, interactionLayer))
        {
            if (hit.collider.CompareTag(interactionTag))
            {
                Debug.Log("Interacted with " + hit.collider.name);
                // Tambahkan kode interaksi di sini
                InteractWithObject(hit.collider.gameObject);
            }
            else
            {
                Debug.Log("No interactable object hit");
            }
        }
        else
        {
            Debug.Log("Nothing hit");
        }
    }

    void InteractWithObject(GameObject obj)
    {
        // Tambahkan logika interaksi dengan objek di sini
        Debug.Log("Interacting with " + obj.name);
    }
}
