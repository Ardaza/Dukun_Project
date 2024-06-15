using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DelayButton : MonoBehaviour
{
    public Button[] buttons; // Array of buttons
    public float delayTime = 3f; // Delay time in seconds
    public float fadeDuration = 1f; // Fade duration in seconds

    private bool buttonsEnabled = true;

    void Start()
    {
        foreach (Button button in buttons)
        {
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            if (canvasGroup == null)
            {
                canvasGroup = button.gameObject.AddComponent<CanvasGroup>();
            }

            canvasGroup.alpha = 0; // Initially make the button invisible
            button.gameObject.SetActive(true); // Ensure the button is active

            // Add click listener to each button
            button.onClick.AddListener(() => OnButtonClick());
        }

        StartCoroutine(FadeInButtons());
    }

    IEnumerator FadeInButtons()
    {
        yield return new WaitForSeconds(delayTime); // Wait for the specified delay time

        foreach (Button button in buttons)
        {
            CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
            float elapsedTime = 0f;

            while (elapsedTime < fadeDuration)
            {
                canvasGroup.alpha = Mathf.Clamp01(elapsedTime / fadeDuration);
                elapsedTime += Time.deltaTime;
                yield return null;
            }

            canvasGroup.alpha = 1f; // Ensure the button is fully visible at the end
        }
    }

    void OnButtonClick()
    {
        if (!buttonsEnabled)
            return;

        buttonsEnabled = false; // Disable further button clicks

        // Fade out all buttons
        foreach (Button button in buttons)
        {
            StartCoroutine(FadeOutButton(button));
        }
    }

    IEnumerator FadeOutButton(Button button)
    {
        CanvasGroup canvasGroup = button.GetComponent<CanvasGroup>();
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            canvasGroup.alpha = Mathf.Clamp01(1 - elapsedTime / fadeDuration); // Fade out
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 0f; // Ensure the button is fully invisible at the end
        button.gameObject.SetActive(false); // Deactivate the button
    }
}