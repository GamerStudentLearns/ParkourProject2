using UnityEngine;

public class LavaBall : MonoBehaviour
{
    public float maxHeight = 20f;
    public GameObject explosionEffect; // Assign in Inspector

    void Update()
    {
        if (transform.position.y >= maxHeight)
        {
            Explode();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hit by lava!");
            Destroy(other.gameObject); // Replace with your death logic
            Explode();
        }
    }

    void Explode()
    {
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}

