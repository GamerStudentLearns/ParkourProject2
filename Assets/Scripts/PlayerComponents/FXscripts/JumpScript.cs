using UnityEngine;
using System.Collections;

public class JumpScript : MonoBehaviour
{
    public AudioClip soundEffect; // Assign this in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Add an AudioSource component if not already present
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundEffect;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(PlaySoundWithDelay());
        }
    }

    IEnumerator PlaySoundWithDelay()
    {
        yield return new WaitForSeconds(1f); // Delay for 1 second
        audioSource.Play();                  // Play the sound
    }
}
