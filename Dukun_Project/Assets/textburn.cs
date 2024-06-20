using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Import the TextMeshPro namespace

public class textburn : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro; // Reference to the TextMeshPro component attached to this object
    public TextMeshProUGUI externalTextMeshPro; // Reference to the TextMeshPro component on another object

    // Start is called before the first frame update
    void Start()
    {
        // Ensure the text is initially empty or hidden
        textMeshPro.text = "";
    }

    // Update is called once per frame
    void Update()
    {

    }

    // This method is called when another collider enters the trigger collider attached to the object where this script is attached
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            // Display the text when an object with the tag "Reach" enters the collider
            textMeshPro.text = "Burn jenglot";

            // Hide the external text when the object with the tag "Reach" enters the collider
            if (externalTextMeshPro != null)
            {
                externalTextMeshPro.text = "";
            }
        }
    }

    // This method is called when another collider exits the trigger collider attached to the object where this script is attached
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Reach"))
        {
            // Hide the text when the object with the tag "Reach" exits the collider
            textMeshPro.text = "";
        }
    }
}
