using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class JenglotManager : MonoBehaviour
{
    private int destroyedObjectsCount = 0;
    public int destroyThreshold = 5; // Number of objects that need to be destroyed to trigger the scene change
    public TextMeshProUGUI thresholdText; // Reference to the TextMeshProUGUI component
    public AudioSource burnAudioSource; // Reference to the AudioSource component

    void Start()
    {
        UpdateThresholdText();
    }

    // Method to increment the destroyed object count and check if it reaches the threshold
    public void CandleLit()
    {
        destroyedObjectsCount++;
        Debug.Log("Candle lit! Total candles: " + destroyedObjectsCount);
        PlayBurnAudio(); // Play the burn audio
        UpdateThresholdText();

        if (destroyedObjectsCount >= destroyThreshold)
        {
            TriggerSceneChange();
        }
    }

    // Method to change the scene
    private void TriggerSceneChange()
    {
        Debug.Log("Threshold reached! Changing scene...");
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene("WinCutscene");
    }

    // Method to update the threshold text
    private void UpdateThresholdText()
    {
        thresholdText.text = "Jenglot : " + destroyedObjectsCount + " / " + destroyThreshold;
    }

    // Method to play the burn audio
    private void PlayBurnAudio()
    {
        if (burnAudioSource != null)
        {
            burnAudioSource.Play();
        }
        else
        {
            Debug.LogWarning("Burn audio source not set.");
        }
    }
}
