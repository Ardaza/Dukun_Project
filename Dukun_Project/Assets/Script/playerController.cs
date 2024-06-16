using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour
{
    public GameObject footstep;

    // Camera
    public Camera playerCam;

    // Camera settings
    public float lookSpeed = 2f;
    public float lookLimit = 75f;
    public float cameraRotation = 2f;

    // Camera zoom
    public int zoomFOV = 35;
    public int initialFOV;
    public float cameraZoom = 1;
    private bool isZoomed = false;

    // Movement
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float gravity = 20f; // Adjusted gravity

    // Ground
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0;
    private float rotationY = 0;

    // Can player move
    private bool canMove = true;

    // Energy bar
    public Slider energyBar;
    public float maxEnergy = 100f;
    private float currentEnergy;
    public float sprintEnergyConsumption = 10f;
    public float energyRegenRate = 5f;
    private bool isRunning = false;

    private CharacterController characterController;

    // Animator for the other object
    public Animator otherObjectAnimator;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        footstep.SetActive(false);

        // Initialize energy
        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;
    }

    void Update()
    {
        if (canMove)
        {
            walkRun();
        }
        cameraMovement();
        UpdateEnergy();
    }

    void walkRun()
    {
        if (characterController.isGrounded)
        {
            // Walk and run
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

            if (isMoving && Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0)
            {
                jalan();
                isRunning = true;
                currentEnergy -= sprintEnergyConsumption * Time.deltaTime;
                currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
            }
            else
            {
                stopJalan();
                isRunning = false;
            }

            float curSpeedX = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical");
            float curSpeedY = (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal");
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void cameraMovement()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
        rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);

        rotationY += Input.GetAxis("Mouse X") * lookSpeed;

        Quaternion targetRotationX = Quaternion.Euler(rotationX, 0, 0);
        Quaternion targetRotationY = Quaternion.Euler(0, rotationY, 0);

        playerCam.transform.localRotation = Quaternion.Slerp(playerCam.transform.localRotation, targetRotationX, Time.deltaTime * cameraRotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationY, Time.deltaTime * cameraRotation);

        if (Input.GetButtonDown("Fire2"))
        {
            isZoomed = true;
        }

        if (Input.GetButtonUp("Fire2"))
        {
            isZoomed = false;
        }

        if (isZoomed)
        {
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, zoomFOV, Time.deltaTime * cameraZoom);
        }
        else
        {
            playerCam.fieldOfView = Mathf.Lerp(playerCam.fieldOfView, initialFOV, Time.deltaTime * cameraZoom);
        }
    }

    void UpdateEnergy()
    {
        if (!isRunning && currentEnergy < maxEnergy)
        {
            currentEnergy += energyRegenRate * Time.deltaTime;
            currentEnergy = Mathf.Clamp(currentEnergy, 0, maxEnergy);
        }

        if (currentEnergy <= 0 && canMove)
        {
            StartCoroutine(EnergyDepleted());
        }

        energyBar.value = currentEnergy;
    }

    IEnumerator EnergyDepleted()
    {
        stopJalan();
        yield return new WaitForSeconds(5);
    }

    void jalan()
    {
        footstep.SetActive(true);
        if (otherObjectAnimator != null)
        {
            otherObjectAnimator.SetBool("run", true);
        }
    }

    void stopJalan()
    {
        footstep.SetActive(false);
        if (otherObjectAnimator != null)
        {
            otherObjectAnimator.SetBool("run", false);
        }
    }
}