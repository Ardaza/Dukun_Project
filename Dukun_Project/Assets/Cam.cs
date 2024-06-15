using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cam : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Transform target;  // Objek yang akan diikuti
    public Vector3 offset;    // Jarak antara kamera dan target

    void LateUpdate()
    {
        // Update posisi kamera sesuai dengan posisi target ditambah offset
        transform.position = target.position + offset;
    }

}
