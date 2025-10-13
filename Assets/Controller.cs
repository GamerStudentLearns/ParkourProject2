using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Controller : MonoBehaviour
{
    public float walkSpeed = 6f;
    public float sprintSpeed = 12f;
    public float slideSpeed = 18f;
    public float slideDuration = 3f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public Transform cameraTransform;
    public float mouseSensitivity = 100f;
    public float cameraDipAmount = 0.5f;
    public float cameraDipSpeed = 5f;

    public float crouchHeight = 1f;
    public float standingHeight = 2f;
    public float crouchCameraOffset = -0.5f;
    public float crouchSpeed = 3f;

    public float rotationSmoothTime = 0.1f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;

    private bool isSliding = false;
    private float slideTimer = 0f;
    private float slideDipProgress = 0f;

    private Vector3 originalCameraLocalPos;
    private Vector3 targetCameraLocalPos;

    private float currentVelocityAngle;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        originalCameraLocalPos = cameraTransform.localPosition;
        targetCameraLocalPos = originalCameraLocalPos;
    }

    void Update()
    {
        HandleCrouch();
        HandleMovement();
        HandleMouseLook();
        HandleJump();
        HandleSlide();
        HandleCameraDip();
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
            velocity.y = -2f;

        float speed = walkSpeed;

        if (isSliding)
        {
            speed = slideSpeed;
        }
        else if (Input.GetKey(KeyCode.C))
        {
            speed = crouchSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = sprintSpeed;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDir = camRight * x + camForward * z;

        if (moveDir.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(moveDir.x, moveDir.z) * Mathf.Rad2Deg;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref currentVelocityAngle, rotationSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        controller.Move(moveDir.normalized * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }

    void HandleJump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    void HandleSlide()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded && !isSliding && !Input.GetKey(KeyCode.C))
        {
            isSliding = true;
            slideTimer = slideDuration;
            slideDipProgress = 0f;
        }

        if (isSliding)
        {
            slideTimer -= Time.deltaTime;
            slideDipProgress = Mathf.Clamp01(slideDipProgress + Time.deltaTime * cameraDipSpeed);
            float dipAmount = Mathf.Lerp(0f, cameraDipAmount, slideDipProgress);
            targetCameraLocalPos = originalCameraLocalPos + Vector3.down * dipAmount;

            if (slideTimer <= 0f)
            {
                isSliding = false;
                targetCameraLocalPos = originalCameraLocalPos;
            }
        }
    }

    void HandleCrouch()
    {
        if (Input.GetKey(KeyCode.C))
        {
            isSliding = false;
            controller.height = crouchHeight;
            targetCameraLocalPos = originalCameraLocalPos + Vector3.up * crouchCameraOffset;
        }
        else
        {
            controller.height = standingHeight;
            if (!isSliding)
                targetCameraLocalPos = originalCameraLocalPos;
        }
    }

    void HandleCameraDip()
    {
        cameraTransform.localPosition = Vector3.Lerp(cameraTransform.localPosition, targetCameraLocalPos, Time.deltaTime * cameraDipSpeed);
    }
}
