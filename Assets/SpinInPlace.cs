using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpinInPlace : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public Vector3 rotationAxis = Vector3.up;
    public float knockbackForce = 10f;

    void Update()
    {
        transform.Rotate(rotationAxis * rotationSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the object has a Rigidbody and is tagged as "Player"
        if (collision.gameObject.CompareTag("Player"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Apply force away from the obstacle's center
                Vector3 forceDirection = (collision.transform.position - transform.position).normalized;
                rb.AddForce(forceDirection * knockbackForce, ForceMode.Impulse);
            }
        }
    }
}
