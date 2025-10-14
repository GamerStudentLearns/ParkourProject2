using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomentumFreeze : MonoBehaviour
{
    private Rigidbody rb;
    private bool isFrozen = false;

    [Header("Movement Settings")]
    public float moveForce = 10f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Check if F is held
        isFrozen = Input.GetKey(KeyCode.F);

        if (isFrozen)
        {
            // Stop horizontal movement only
            rb.velocity = new Vector3(0f, rb.velocity.y, 0f);
            rb.angularVelocity = Vector3.zero;
        }
    }

    void FixedUpdate()
    {
        if (isFrozen)
        {
            // Skip movement input while frozen
            return;
        }

        // Apply movement force
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(x, 0f, z) * moveForce;

        rb.AddForce(move, ForceMode.Acceleration);
    }
}
