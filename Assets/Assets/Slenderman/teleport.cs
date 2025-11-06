using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    // Array of teleport points
    public Transform[] teleportPoints;

    // Time between teleports
    public float teleportInterval = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= teleportInterval)
        {
            TeleportToRandomPoint();
            timer = 0f;
        }
    }

    void TeleportToRandomPoint()
    {
        if (teleportPoints.Length == 0) return;

        int randomIndex = Random.Range(0, teleportPoints.Length);
        transform.position = teleportPoints[randomIndex].position;
    }
}


