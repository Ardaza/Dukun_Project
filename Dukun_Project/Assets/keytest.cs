using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Key : MonoBehaviour
{
    public Doors doorScript;
    public GameObject pickupText;
    public TextMeshProUGUI pickupTextComponent;
    public string pickupMessage = "Key";
    public AudioSource pickupSound;

    private bool inReach;

    void Start()
    {
        inReach = false;
        pickupText.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickupText.SetActive(true);
            pickupTextComponent.text = "[E] " + gameObject.name; // Menampilkan nama objek kunci di Hierarchy menggunakan TextMeshPro
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickupText.SetActive(false);
            pickupTextComponent.text = "";
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
        doorScript.UnlockDoor();
        pickupText.SetActive(false);
        pickupSound.Play();
        Destroy(gameObject, pickupSound.clip.length);
    }
}
