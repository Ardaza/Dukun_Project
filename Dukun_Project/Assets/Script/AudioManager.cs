using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public AudioClip gameplayBGM;
    public AudioClip defaultBGM;
    public VideoPlayer videoPlayer; // Referensi ke VideoPlayer

    private static AudioManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        // Memutuskan lagu berdasarkan scene yang aktif saat dimulai
        PlayMusicForScene(SceneManager.GetActiveScene().buildIndex);
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached += OnVideoEnd; // Tambahkan event listener
            videoPlayer.started += OnVideoStart; // Tambahkan event listener
        }
    }

    private void OnEnable()
    {
        // Mendaftarkan pendengar untuk event scene loaded
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // Melepaskan pendengar untuk event scene loaded untuk menghindari memory leak
        SceneManager.sceneLoaded -= OnSceneLoaded;
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoEnd; // Lepaskan event listener
            videoPlayer.started -= OnVideoStart; // Lepaskan event listener
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Memutuskan lagu berdasarkan scene yang baru dimuat
        PlayMusicForScene(scene.buildIndex);
    }

    void PlayMusicForScene(int sceneBuildIndex)
    {
        // Memutuskan lagu berdasarkan sceneBuildIndex
        if (sceneBuildIndex == 5 || sceneBuildIndex == 6)
        {
            // Hancurkan AudioManager jika di scene 5 atau 6
            Destroy(gameObject);
        }
        else if (sceneBuildIndex == 4)
        {
            // Memainkan lagu gameplay jika di scene 4
            if (musicSource.clip != gameplayBGM)
            {
                musicSource.clip = gameplayBGM;
                musicSource.Play();
            }
        }
        else if (sceneBuildIndex == 0 || sceneBuildIndex == 1)
        {
            // Memainkan lagu default jika di scene 0 atau 1
            if (musicSource.clip != defaultBGM)
            {
                musicSource.clip = defaultBGM;
                musicSource.Play();
            }
        }
        else
        {
            // Stop musik untuk scene lainnya
            DontDestroyOnLoad(gameObject);
        }
    }

    void OnVideoStart(VideoPlayer vp)
    {
        // Berhenti musik saat video dimulai
        if (musicSource.isPlaying)
        {
            musicSource.Pause();
        }
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Lanjutkan musik saat video selesai diputar
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}