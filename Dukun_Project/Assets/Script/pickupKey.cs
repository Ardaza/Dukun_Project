using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickupKey : MonoBehaviour
{
    public GameObject keyOb;
    public GameObject invOb;
    public GameObject pickUpText;
    public AudioSource keySound;

    public bool inReach;
    // Start is called before the first frame update
    void Start()
    {
        inReach = false;
        pickUpText.SetActive(false);
        invOb.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inReach && Input.GetButtonDown("Interact")) 
        {
            keyOb.SetActive(false);
            keySound.Play();
            invOb.SetActive(true);
            pickUpText.SetActive(false);   
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = true;
            pickUpText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Reach")
        {
            inReach = false;
            pickUpText.SetActive(false);
        }
    }
}
