using UnityEngine;
using System.Collections.Generic;

public class LavaLauncher : MonoBehaviour
{
    public GameObject lavaBallPrefab;
    public Transform[] spawnPoints;
    public float launchForce = 15f;
    public float launchInterval = 3f;

    private List<GameObject> activeLavaBalls = new List<GameObject>();

    void Start()
    {
        InvokeRepeating(nameof(LaunchLavaBalls), 0f, launchInterval);
    }

    void LaunchLavaBalls()
    {
        // Destroy all previously spawned lava balls
        foreach (GameObject ball in activeLavaBalls)
        {
            if (ball != null)
            {
                Destroy(ball);
            }
        }
        activeLavaBalls.Clear();

        // Spawn new lava balls at each spawn point
        foreach (Transform spawnPoint in spawnPoints)
        {
            GameObject lavaBall = Instantiate(lavaBallPrefab, spawnPoint.position, Quaternion.identity);
            Rigidbody rb = lavaBall.GetComponent<Rigidbody>();
            rb.linearVelocity = Vector3.up * launchForce;

            activeLavaBalls.Add(lavaBall);
        }
    }
}


