using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System.Collections;

public class JumpscareTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false); // Menyembunyikan canvas di awal
    }

    private IEnumerator OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true); // Menampilkan canvas saat pemain masuk ke trigger
            yield return new WaitForSeconds(4f);
            videoPlayer.Play(); // Memutar video jumpscare
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene(0);
        }
    }
}
