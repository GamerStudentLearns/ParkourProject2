using UnityEngine;

public class SidewaysShooter : MonoBehaviour
{
    [Header("Projectile Settings")]
    public GameObject projectilePrefab;   // The object to shoot
    public float shootForce = 10f;        // Force applied sideways
    public float fireRate = 2f;           // Time between shots

    [Header("Direction Settings")]
    public bool shootRight = true;        // Toggle direction (right or left)

    private float nextFireTime = 0f;

    void Update()
    {
        if (Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        if (projectilePrefab == null) return;

        // Spawn projectile at shooter’s position
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Add Rigidbody if not already present
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = projectile.AddComponent<Rigidbody>();
        }

        // Decide direction
        Vector3 direction = shootRight ? Vector3.right : Vector3.left;

        // Apply force sideways
        rb.AddForce(direction * shootForce, ForceMode.Impulse);

        // Optional: destroy projectile after 5 seconds to avoid clutter
        Destroy(projectile, 5f);
    }
}
