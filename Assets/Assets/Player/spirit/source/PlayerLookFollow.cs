using UnityEngine;

public class PlayerLookFollow : MonoBehaviour
{
    [Header("References")]
    [Tooltip("The camera whose direction the player should follow.")]
    public Transform cameraTransform;

    [Tooltip("The part of the player model that should rotate (e.g., the body root).")]
    public Transform playerModel;

    [Header("Settings")]
    [Tooltip("How quickly the player model rotates to match the camera.")]
    public float rotationSpeed = 10f;

    void Update()
    {
        if (cameraTransform == null || playerModel == null) return;

        // Get the forward direction of the camera, ignoring vertical tilt
        Vector3 lookDirection = cameraTransform.forward;
        lookDirection.y = 0f; // Prevents the player from tilting up/down

        if (lookDirection.sqrMagnitude > 0.01f)
        {
            // Target rotation based on camera direction
            Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

            // Smoothly rotate the player model
            playerModel.rotation = Quaternion.Slerp(
                playerModel.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime
            );
        }
    }
}

