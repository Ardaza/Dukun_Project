using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BudiAnim : MonoBehaviour
{
    private Animator animator;
    private bool isWalking;
    private bool isRunning;
    private bool isInteracting;
    private bool isUsingLighter;
    private bool isReloading;  // New boolean for reload animation

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalking = false;
        isRunning = false;
        isInteracting = false;
        isUsingLighter = false;
        isReloading = false;  // Initialize reload boolean
    }

    // Update is called once per frame
    void Update()
    {
        HandleInteractionInput();
        HandleMovementInput();
        HandleReloadInput();  // Check for reload input
        UpdateAnimator();
    }

    void HandleMovementInput()
    {
        // Reset walking and running if interacting or using lighter
        if (isInteracting || isUsingLighter || isReloading)  // Include reload condition
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

        // Check for lighter input (Q)
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isUsingLighter = true;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            isUsingLighter = false;
        }
    }

    void HandleReloadInput()
    {
        // Check for reload input (R)
        if (Input.GetKeyDown(KeyCode.R))
        {
            isReloading = true;
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            isReloading = false;
        }
    }

    void UpdateAnimator()
    {
        animator.SetBool("walk", isWalking);
        animator.SetBool("interact", isInteracting);
        animator.SetBool("lighter", isUsingLighter);
        animator.SetBool("reload", isReloading);  // Update reload boolean
    }
}
