using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JenglotManager : MonoBehaviour
{
    private int litJenglotCount = 0;

    // This method is called when a candle is lit
    public void CandleLit()
    {
        litJenglotCount++;

        // Check if at least one candle has been lit
        if (litJenglotCount == 5)
        {
            // Load the scene with build index 1
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(6);
        }
    }
}