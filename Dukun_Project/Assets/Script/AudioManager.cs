using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource musicSource;
    public AudioClip gameplayBGM;
    public AudioClip defaultBGM;

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
            // Memainkan lagu gameplay jika di scene 1
            if (musicSource.clip != gameplayBGM)
            {
                musicSource.clip = gameplayBGM;
                musicSource.Play();
            }
        }
        else if (sceneBuildIndex == 0 || sceneBuildIndex == 1)
        {
            // Memainkan lagu default jika di scene 4
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
}