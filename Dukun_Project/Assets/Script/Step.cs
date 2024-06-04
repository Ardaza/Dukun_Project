using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stepScript : MonoBehaviour
{
    public GameObject step;
    public float stepCooldown = 0.5f; // Cooldown time in seconds

    private bool isMoving = false;
    private float nextStepTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        step.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
    }

    void HandleMovement()
    {
        bool movementKeyPressed = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

        if (movementKeyPressed)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Stopsteps(); // Stop steps when running
            }
            else
            {
                if (Time.time >= nextStepTime)
                {
                    steps();
                }
            }
        }
        else
        {
            Stopsteps();
        }

        isMoving = movementKeyPressed;
    }

    void steps()
    {
        if (!step.activeSelf) // Only activate if not already active
        {
            step.SetActive(true);
            nextStepTime = Time.time + stepCooldown; // Set the next allowed step time
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
