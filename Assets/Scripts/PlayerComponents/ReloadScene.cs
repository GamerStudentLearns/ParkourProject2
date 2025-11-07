using UnityEngine;

public class TeleportToStart : MonoBehaviour
{
    public Transform player;               // Assign in Inspector or auto-find by tag
    public AudioClip teleportSound;        // Assign a sound clip in the Inspector
    private AudioSource audioSource;       // Audio source to play the clip

    private Vector3 startPosition;         // Starting position
    private Quaternion startRotation;      // Starting rotation

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }

        startPosition = player.position;
        startRotation = player.rotation;

        // Create or get AudioSource
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Plane"))
        {
            player.position = startPosition;
            player.rotation = startRotation;

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            if (teleportSound != null)
            {
                audioSource.PlayOneShot(teleportSound);
            }
        }
    }
}


