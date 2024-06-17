using UnityEngine;

public class ReachTestBang : MonoBehaviour
{
    private void Start()
    {
        // Pada awal permainan, atur tag objek menjadi "Untagged"
        gameObject.tag = "Untagged";
    }

    private void OnTriggerEnter(Collider other)
    {
        // Cek jika objek bersentuhan dengan objek yang memiliki tag "interact"
        if (other.CompareTag("interact"))
        {
            // Ubah tag objek menjadi "Reach"
            gameObject.tag = "Reach";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Cek jika objek tidak lagi bersentuhan dengan objek yang memiliki tag "interact"
        if (other.CompareTag("interact"))
        {
            // Ubah tag objek menjadi "Untagged"
            gameObject.tag = "Untagged";
        }
    }
}
