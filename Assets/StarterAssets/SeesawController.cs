using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SeesawController : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Apply torque based on contact point
        Vector3 contactPoint = collision.contacts[0].point;
        Vector3 center = transform.position;
        float direction = contactPoint.x < center.x ? 1f : -1f;

        rb.AddTorque(Vector3.forward * direction * 50f, ForceMode.Impulse);
    }
}
