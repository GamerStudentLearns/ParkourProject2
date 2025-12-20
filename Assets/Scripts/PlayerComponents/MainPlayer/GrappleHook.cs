using UnityEngine;

public class GrappleHook : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;        // Assign your player camera
    public Transform gunTip;           // Rope start position (e.g., weapon tip)
    public LineRenderer lineRenderer;  // Rope visual
    public Rigidbody playerRb;         // Player Rigidbody

    [Header("Settings")]
    [SerializeField] private float maxDistance = 30f;   // Grapple range
    [SerializeField] private float pullSpeed = 20f;     // Speed of pull (editable in Inspector)
    [SerializeField] private LayerMask grappleLayer;    // Valid grapple layers
    [SerializeField] private float stopDistance = 1f;   // Distance at which grapple auto-stops
    [SerializeField] private float ropeWidth = 0.05f;   // Rope thickness

    [Header("Visual Effects")]
    [SerializeField] private ParticleSystem speedLines; // Assign particle system prefab in Inspector

    [Header("Activation")]
    [SerializeField] private GameObject objectToActivate; // Object to toggle when grappling

    private Vector3 grapplePoint;
    private bool isGrappling = false;

    void Start()
    {
        // Initialize rope width
        lineRenderer.startWidth = ropeWidth;
        lineRenderer.endWidth = ropeWidth;
        lineRenderer.positionCount = 0;

        if (speedLines != null)
            speedLines.Stop();

        if (objectToActivate != null)
            objectToActivate.SetActive(false);
    }

    void Update()
    {
        // Fire grapple
        if (Input.GetMouseButtonDown(0) && !isGrappling)
        {
            Collider[] hits = Physics.OverlapSphere(playerRb.position, maxDistance, grappleLayer);

            if (hits.Length > 0)
            {
                // Find closest grapple point
                Transform closest = hits[0].transform;
                float closestDist = Vector3.Distance(playerRb.position, closest.position);

                foreach (Collider c in hits)
                {
                    float dist = Vector3.Distance(playerRb.position, c.transform.position);
                    if (dist < closestDist)
                    {
                        closest = c.transform;
                        closestDist = dist;
                    }
                }

                StartGrapple(closest.position);
            }
        }

        // Release grapple
        if (Input.GetMouseButtonUp(0) && isGrappling)
        {
            StopGrapple();
        }

        // Rope visual update
        if (isGrappling)
        {
            lineRenderer.SetPosition(0, gunTip.position);
            lineRenderer.SetPosition(1, grapplePoint);
        }
    }

    void FixedUpdate()
    {
        if (isGrappling)
        {
            // Move directly toward grapple point
            playerRb.MovePosition(
                Vector3.MoveTowards(playerRb.position, grapplePoint, pullSpeed * Time.fixedDeltaTime)
            );

            // Update speed lines orientation + intensity
            if (speedLines != null)
            {
                Vector3 vel = playerRb.linearVelocity;

                if (vel.sqrMagnitude > 0.01f)
                {
                    // Rotate particle system so emission is opposite velocity
                    speedLines.transform.rotation = Quaternion.LookRotation(-vel.normalized);

                    // Scale emission rate with speed
                    var emission = speedLines.emission;
                    emission.rateOverTime = vel.magnitude * 3f;
                }
            }

            // Stop when close enough
            if (Vector3.Distance(playerRb.position, grapplePoint) < stopDistance)
            {
                StopGrapple();
            }
        }
    }

    void StartGrapple(Vector3 point)
    {
        grapplePoint = point;
        isGrappling = true;

        // Enable rope visual
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, gunTip.position);
        lineRenderer.SetPosition(1, grapplePoint);

        if (speedLines != null)
            speedLines.Play();

        // Activate object
        if (objectToActivate != null)
            objectToActivate.SetActive(true);

        Debug.Log("Started grapple at " + point);
    }

    void StopGrapple()
    {
        isGrappling = false;
        lineRenderer.positionCount = 0;

        if (speedLines != null)
            speedLines.Stop();

        // Deactivate object
        if (objectToActivate != null)
            objectToActivate.SetActive(false);

        Debug.Log("Stopped grapple");
    }
}

