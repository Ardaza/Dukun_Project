using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    private JenglotManager jenglotManager;

    private void Start()
    {
        jenglotManager = FindObjectOfType<JenglotManager>();
        if (jenglotManager == null)
        {
            Debug.LogError("JenglotManager tidak ditemukan di scene.");
        }
    }

    // Method ini dipanggil ketika objek ini bertabrakan dengan collider lain
    private void OnCollisionEnter(Collision collision)
    {
        // Memeriksa apakah objek bertabrakan dengan GameObject yang ditag sebagai "Fire"
        if (collision.gameObject.CompareTag("Fire"))
        {
            // Menghancurkan GameObject ini
            Destroy(gameObject);

            // Menambahkan nilai CandleLit di JenglotManager
            if (jenglotManager != null)
            {
                jenglotManager.CandleLit();
            }
            else
            {
                Debug.LogWarning("JenglotManager belum diinisialisasi.");
            }
        }
    }
}