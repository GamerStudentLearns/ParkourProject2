using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LookAt : MonoBehaviour
{
    public Transform player; // Assign the player's transform in the Inspector

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = player.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);

        }
    }
}


