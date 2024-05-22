using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepScript : MonoBehaviour
{
    public GameObject step;

    // Start is called before the first frame update
    void Start()
    {
        step.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Stopsteps(); // Assuming you want to stop steps when running
            }
            else
            {
                steps();
            }
        }
        else
        {
            Stopsteps();
        }
    }

    void steps()
    {
        if (!step.activeSelf) // Only activate if not already active
        {
            step.SetActive(true);
        }
    }

    void Stopsteps()
    {
        if (step.activeSelf) // Only deactivate if not already inactive
        {
            step.SetActive(false);
        }
    }
}
