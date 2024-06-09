using UnityEngine;
using UnityEngine.Video;

public class JumpscareTrigger : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject canvas;

    private void Start()
    {
        canvas.SetActive(false); // Menyembunyikan canvas di awal
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true); // Menampilkan canvas saat pemain masuk ke trigger
            videoPlayer.Play(); // Memutar video jumpscare
        }
    }
}
