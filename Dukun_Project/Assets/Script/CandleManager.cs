using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CandleManager : MonoBehaviour
{
    private int litCandleCount = 0;

    // This method is called when a candle is lit
    public void CandleLit()
    {
        litCandleCount++;

        // Check if at least one candle has been lit
        if (litCandleCount == 5)
        {
            // Load the scene with build index 1
            SceneManager.LoadScene(1);
        }
    }
}