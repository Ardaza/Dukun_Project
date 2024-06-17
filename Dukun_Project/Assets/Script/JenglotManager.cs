using UnityEngine;
using UnityEngine.SceneManagement;

public class JenglotManager : MonoBehaviour
{
    private int litJenglotCount = 0;

    // Method ini dipanggil ketika jenglot dinyalakan
    public void CandleLit()
    {
        litJenglotCount++;

        if (litJenglotCount == 11)
        {
            // Load scene dengan build index 7
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(6);
        }
    }
}