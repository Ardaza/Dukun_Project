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

    // camera zoom
    public int zoomFOV = 35;
    public int initialFOV;
    public float cameraZoom = 1;
    private bool isZoomed = false;

    // Movement
    public float walkSpeed = 3f;
    public float runSpeed = 5f;
    public float gravity = 10f;

    // Ground
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    float rotationY = 0;

    // Can player move
    private bool canMove = true;

    CharacterController characterController;
    void Start()
    {
        //
        characterController = GetComponent<CharacterController>();

        // kursor hilang
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = true;

        footstep.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        walkRun();
        cameraMovement();
    }

    void walkRun()
    {
        // jalan dan lari
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        bool isRunning;

        if (isRunning = Input.GetKey(KeyCode.LeftShift)) 
        {
            jalan();
            isRunning = true;
        }
        else
        {
            stopJalan();
            isRunning = false;
        }

        float curSpeedX = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runSpeed : walkSpeed) * Input.GetAxis("Horizontal") : 0;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

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
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, zoomFOV, Time.deltaTime * cameraZoom);

        }
        else
        {
            playerCam.GetComponent<Camera>().fieldOfView = Mathf.Lerp(playerCam.fieldOfView, initialFOV, Time.deltaTime * cameraZoom);
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
