using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour
{

    public GameObject object1;
    public GameObject object2;
    public GameObject object3;

    // Start is called before the first frame update
    void Start()
    {
        object1.SetActive(false);
        object2.SetActive(false);
        object3.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("1"))
        {
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(false);

        }

        if (Input.GetButtonDown("2"))
        {
            object1.SetActive(true);
            object2.SetActive(false);
            object3.SetActive(false);

        }

        if (Input.GetButtonDown("3"))
        {
            object1.SetActive(false);
            object2.SetActive(true);
            object3.SetActive(false);

        }

        if (Input.GetButtonDown("4"))
        {
            object1.SetActive(false);
            object2.SetActive(false);
            object3.SetActive(true);
        }
    }
}
