using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
    [Header("References")]
    public CharacterController controller;
    public Transform cameraTransform;
    public Transform playerModel;

    [Header("Movement Settings")]
    public float walkSpeed = 6f;
    public float slideSpeed = 12f;
    public float jumpHeight = 2f;
    public float gravity = -9.81f;
    public float wallRunGravity = -2f;
    public float wallRunDuration = 1.5f;
    public float wallJumpForce = 8f;

    [Header("Camera Settings")]
    public float mouseSensitivity = 100f;
    public Vector3 cameraOffset = new Vector3(0f, 2f, -4f);
    public float cameraSmoothSpeed = 10f;
    public LayerMask cameraCollisionMask;
    public float cameraCollisionRadius = 0.3f;

    private Vector3 velocity;
    private bool isGrounded;
    private bool isSliding = false;
    private bool isWallRunning = false;
    private float wallRunTimer = 0f;
    private Vector3 wallNormal;

    private float xRotation = 0f;
    private float yRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Vector3 angles = transform.eulerAngles;
        xRotation = angles.x;
        yRotation = angles.y;
    }

    void Update()
    {
        HandleCamera();
        HandleMovement();
        HandleJumping();
        HandleSliding();
        HandleWallRunning();
    }

    void HandleCamera()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -30f, 60f);

        Quaternion camRotation = Quaternion.Euler(xRotation, yRotation, 0f);
        Vector3 desiredCameraPos = transform.position + camRotation * cameraOffset;

        // Camera collision
        Vector3 directionToCamera = (desiredCameraPos - transform.position).normalized;
        float distanceToCamera = cameraOffset.magnitude;

        if (Physics.SphereCast(transform.position, cameraCollisionRadius, directionToCamera, out RaycastHit hit, distanceToCamera, cameraCollisionMask))
        {
            desiredCameraPos = transform.position + directionToCamera * (hit.distance - cameraCollisionRadius);
        }

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, desiredCameraPos, cameraSmoothSpeed * Time.deltaTime);
        cameraTransform.rotation = camRotation;
    }

    void HandleMovement()
    {
        isGrounded = controller.isGrounded;
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = Quaternion.Euler(0f, yRotation, 0f) * new Vector3(x, 0f, z);

        float currentSpeed = isSliding ? slideSpeed : walkSpeed;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Rotate player model to face movement direction
        if (move.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move);
            playerModel.rotation = Quaternion.Slerp(playerModel.rotation, targetRotation, 10f * Time.deltaTime);
        }
    }

    void HandleJumping()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isWallRunning)
            {
                velocity = wallNormal * wallJumpForce;
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
                isWallRunning = false;
            }
            else if (isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            }
        }

        velocity.y += (isWallRunning ? wallRunGravity : gravity) * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void HandleSliding()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && isGrounded)
        {
            isSliding = true;
            playerModel.localScale = new Vector3(1f, 0.5f, 1f);
        }
        if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            isSliding = false;
            playerModel.localScale = Vector3.one;
        }
    }

    void HandleWallRunning()
    {
        if (isGrounded || isSliding) return;

        RaycastHit hit;
        Vector3[] directions = { transform.right, -transform.right };

        foreach (Vector3 dir in directions)
        {
            if (Physics.Raycast(transform.position, dir, out hit, 1f))
            {
                if (Input.GetKey(KeyCode.Space))
                {
                    isWallRunning = true;
                    wallRunTimer = wallRunDuration;
                    wallNormal = hit.normal;
                    velocity.y = 0f;
                    break;
                }
            }
        }

        if (isWallRunning)
        {
            wallRunTimer -= Time.deltaTime;
            if (wallRunTimer <= 0 || isGrounded)
            {
                isWallRunning = false;
            }
        }
    }
}


