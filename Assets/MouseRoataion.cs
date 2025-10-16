using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRoataion : MonoBehaviour
{
    public float mouseSensitivity = 5f;
    public Transform orientation; // Optional: helps align other objects with camera direction

    private float rotationX = 0f;
    private float rotationY = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        rotationY += mouseX;
        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -80f, 80f);

        transform.rotation = Quaternion.Euler(rotationX, rotationY, 0f);

        if (orientation != null)
        {
            orientation.rotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }
}
