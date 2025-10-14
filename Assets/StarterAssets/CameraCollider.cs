using UnityEngine;


public class CameraCollision : MonoBehaviour
{
    public Transform player;
    public float distance = 5.0f;
    public float minDistance = 1.0f;
    public float smooth = 10.0f;
    public LayerMask collisionLayers;

    private Vector3 desiredPosition;

    void LateUpdate()
    {
        Vector3 offset = -player.forward * distance;
        Vector3 desiredPosition = player.position + offset;
        Vector3 direction = offset.normalized;

        RaycastHit hit;

        desiredPosition = player.position - direction * distance;

        if (Physics.Raycast(player.position, direction, out hit, distance, collisionLayers))
        {
            float hitDistance = Mathf.Clamp(hit.distance, minDistance, distance);
            desiredPosition = player.position - direction * hitDistance;
        }

        transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * smooth);
        transform.LookAt(player);
    }
}
