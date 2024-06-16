using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpsceneTrigger : MonoBehaviour
{
    // Nama scene yang ingin ditampilkan saat player menyentuh collider
    public string sceneName;

    // Method ini akan dipanggil saat ada objek yang masuk ke dalam collider
    private void OnTriggerEnter(Collider other)
    {
        // Memeriksa apakah objek yang masuk ke dalam collider adalah player
        if (other.CompareTag("Player"))
        {
            // Memuat scene yang ditentukan
            SceneManager.LoadScene(sceneName);
        }
    }
}
