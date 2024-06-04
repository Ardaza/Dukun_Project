using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        // Hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        footstep.SetActive(false);
    }

    void Update()
    {
        walkRun();
        cameraMovement();
    }

    void walkRun()
    {
        if (characterController.isGrounded)
        {
            // Walk and run
            Vector3 forward = transform.TransformDirection(Vector3.forward);
            Vector3 right = transform.TransformDirection(Vector3.right);

            bool isRunning = false;
            bool isMoving = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D);

            if (isMoving && Input.GetKey(KeyCode.LeftShift))
            {
                jalan();
                isRunning = true;
            }
            else
            {
                stopJalan();
            }

            float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
            float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
            moveDirection = (forward * curSpeedX) + (right * curSpeedY);
        }

        // Apply gravity
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the character
        characterController.Move(moveDirection * Time.deltaTime);
    }

    void cameraMovement()
    {
        if (canMove)
        {
            rotationX -= Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookLimit, lookLimit);

            rotationY += Input.GetAxis("Mouse X") * lookSpeed;

            Quaternion targetRotationX = Quaternion.Euler(rotationX, 0, 0);
            Quaternion targetRotationY = Quaternion.Euler(0, rotationY, 0);

            playerCam.transform.localRotation = Quaternion.Slerp(playerCam.transform.localRotation, targetRotationX, Time.deltaTime * cameraRotation);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotationY, Time.deltaTime * cameraRotation);
        }

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

    void jalan()
    {
        footstep.SetActive(true);
    }

    void stopJalan()
    {
        footstep.SetActive(false);
    }
}
