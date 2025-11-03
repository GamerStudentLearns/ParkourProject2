using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class camcon : MonoBehaviour
{
    public Transform target; // Player transform
    public float mouseSensitivity = 100f;
    public float distance = 4f;
    public float height = 2f;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Rotate player horizontally
        target.Rotate(Vector3.up * mouseX);

        // Tilt camera vertically
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -45f, 45f);

        // Calculate camera position behind player
        Vector3 offset = Quaternion.Euler(xRotation, target.eulerAngles.y, 0f) * new Vector3(0, height, -distance);
        transform.position = target.position + offset;

        // Look at the player
        transform.LookAt(target.position + Vector3.up * height);
    }
}

