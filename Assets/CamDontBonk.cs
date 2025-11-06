using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;

public class CamDontBonk : MonoBehaviour
{
    public Transform target; // The object the camera follows
    public float distance = 5.0f; // Default distance from the target
    public float smoothSpeed = 10.0f; // Smooth movement speed
    public LayerMask collisionLayers; // Layers considered as obstacles

    private Vector3 currentVelocity;

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position - target.forward * distance;
        Vector3 direction = desiredPosition - target.position;

        // Raycast from target to desired camera position
        if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, distance, collisionLayers))
        {
            // Move camera to hit point, slightly offset to avoid clipping
            Vector3 hitPosition = hit.point + hit.normal * 0.3f;
            transform.position = Vector3.SmoothDamp(transform.position, hitPosition, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }
        else
        {
            // No obstacle, move to desired position
            transform.position = Vector3.SmoothDamp(transform.position, desiredPosition, ref currentVelocity, Time.deltaTime * smoothSpeed);
        }

        transform.LookAt(target);
    }
}

