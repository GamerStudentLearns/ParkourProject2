using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisappearReappear : MonoBehaviour
{
    public float delay = 3f; // Time in seconds before reappearing

    void Start()
    {
        StartCoroutine(DisappearAndReappear());
    }

    IEnumerator DisappearAndReappear()
    {
        // Make object disappear
        gameObject.SetActive(false);

        // Wait for the delay
        yield return new WaitForSeconds(delay);

        // Make object reappear
        gameObject.SetActive(true);
    }
}

