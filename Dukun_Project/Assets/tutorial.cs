using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ToggleImageScript : MonoBehaviour
{
    public Image toggleImage;  // Reference to the Image to be toggled
    public GameObject crosshair;  // Reference to the Crosshair GameObject
    public float transitionDuration = 0.5f;  // Duration of the fade transition
    public AudioSource audioSource;  // Reference to the AudioSource component
    public AudioClip toggleSound;  // Reference to the AudioClip to be played

    private CanvasGroup canvasGroup;
    private bool isTransitioning = false;

    void Start()
    {
        if (toggleImage != null)
        {
            canvasGroup = toggleImage.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = toggleImage.gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0f;  // Initially hide the image
            toggleImage.gameObject.SetActive(false);  // Ensure the image is inactive at start
        }

        if (crosshair != null)
        {
            crosshair.SetActive(true);  // Initially show the crosshair
        }

        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && !isTransitioning)
        {
            if (toggleImage != null && crosshair != null)
            {
                bool isImageActive = toggleImage.gameObject.activeSelf;
                StartCoroutine(ToggleImageWithTransition(!isImageActive));
            }
        }
    }

    private IEnumerator ToggleImageWithTransition(bool show)
    {
        isTransitioning = true;

        // Play the toggle sound if it is assigned
        if (toggleSound != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }

        if (show)
        {
            toggleImage.gameObject.SetActive(true);
            crosshair.SetActive(false);
        }

        float elapsedTime = 0f;
        float startAlpha = canvasGroup.alpha;
        float endAlpha = show ? 1f : 0f;

        while (elapsedTime < transitionDuration)
        {
            elapsedTime += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / transitionDuration);
            yield return null;
        }

        canvasGroup.alpha = endAlpha;

        if (!show)
        {
            toggleImage.gameObject.SetActive(false);
            crosshair.SetActive(true);
        }

        isTransitioning = false;
    }
}
