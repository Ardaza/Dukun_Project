using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class VideoSceneSwitcher : MonoBehaviour
{
    public string nextSceneName;
    private VideoPlayer videoPlayer;

    void Start()
    {
        // Mendapatkan komponen VideoPlayer
        videoPlayer = GetComponent<VideoPlayer>();

        // Menambahkan event handler untuk ketika video selesai diputar
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Berpindah ke scene berikutnya setelah video selesai diputar
        SceneManager.LoadScene(nextSceneName);
    }
}