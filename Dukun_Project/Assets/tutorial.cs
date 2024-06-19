using UnityEngine;
using UnityEngine.UI;

public class ToggleImageScript : MonoBehaviour
{
    public Image toggleImage;  // Reference to the Image to be toggled
    public GameObject crosshair;  // Reference to the Crosshair GameObject

    void Start()
    {
        if (toggleImage != null)
        {
            toggleImage.gameObject.SetActive(false);  // Initially hide the image
        }

        if (crosshair != null)
        {
            crosshair.SetActive(true);  // Initially show the crosshair
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (toggleImage != null && crosshair != null)
            {
                bool isImageActive = toggleImage.gameObject.activeSelf;
                toggleImage.gameObject.SetActive(!isImageActive);
                crosshair.SetActive(isImageActive);  // Toggle crosshair based on the image's active state
            }
        }
    }
}
