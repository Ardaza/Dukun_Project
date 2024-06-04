using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothViewBobbing : MonoBehaviour
{
    public float bobbingSpeed = 0.18f;
    public float bobbingAmount = 0.2f;
    public float rotationAmount = 1.0f; // Amount of rotation in degrees
    public float midpoint = 2.0f;
    public float normalFOV = 60f; // Normal Field of View
    public float sprintFOV = 75f; // Field of View when sprinting
    public float fovTransitionSpeed = 5f; // Speed of FOV transition

    private float timer = 0.0f;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Camera playerCamera;

    void Start()
    {
        initialPosition = transform.localPosition;
        initialRotation = transform.localRotation;
        playerCamera = GetComponent<Camera>();
        if (playerCamera == null)
        {
            Debug.LogError("No Camera component found on this GameObject. Please add a Camera component.");
        }
    }

    void Update()
    {
        // Handle View Bobbing
        float waveslice = 0.0f;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool isMoving = Mathf.Abs(horizontal) > 0 || Mathf.Abs(vertical) > 0;

        if (!isMoving)
        {
            timer = 0.0f;
        }
        else
        {
            waveslice = Mathf.Sin(timer);
            timer += bobbingSpeed;
            if (timer > Mathf.PI * 2)
            {
                timer -= Mathf.PI * 2;
            }
        }

        if (waveslice != 0)
        {
            float translateChange = waveslice * bobbingAmount;
            float rotateChange = waveslice * rotationAmount;
            float totalAxes = Mathf.Abs(horizontal) + Mathf.Abs(vertical);
            totalAxes = Mathf.Clamp(totalAxes, 0.0f, 1.0f);
            translateChange = totalAxes * translateChange;
            rotateChange = totalAxes * rotateChange;
            transform.localPosition = new Vector3(initialPosition.x, initialPosition.y + translateChange, initialPosition.z);
            transform.localRotation = initialRotation * Quaternion.Euler(0, rotateChange, 0);
        }
        else
        {
            transform.localPosition = initialPosition;
            transform.localRotation = initialRotation;
        }

        // Handle FOV change
        if (isMoving && Input.GetKey(KeyCode.LeftShift))
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, sprintFOV, Time.deltaTime * fovTransitionSpeed);
        }
        else
        {
            playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, normalFOV, Time.deltaTime * fovTransitionSpeed);
        }
    }
}
