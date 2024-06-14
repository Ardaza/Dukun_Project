using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudiAnim : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;
    private bool isRunning;
    private bool isInteracting;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = false;
        isRunning = false;
        isInteracting = false;
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteractionInput();  // Check for interaction first
        HandleMovementInput();     // Then check for movement
        UpdateAnimator();
    }

    void HandleMovementInput()
    {
        // Reset walking and running if interacting
        if (isInteracting)
        {
            isWalking = false;
            isRunning = false;
            return;
        }

        // Check for walk input (WASD)
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalking = true;
        }
        else
        {
            isWalking = false;
        }

        // Check for run input (Left Shift)
        if (Input.GetKey(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }

    void HandleInteractionInput()
    {
        // Check for interaction input (E)
        if (Input.GetKeyDown(KeyCode.E))
        {
            isInteracting = true;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            isInteracting = false;
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("walk", isWalking);
        animator.SetBool("run", isRunning);
        animator.SetBool("interact", isInteracting);
    }
}
