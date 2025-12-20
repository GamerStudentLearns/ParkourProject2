using UnityEngine;

public class SeesawTiltAxis : MonoBehaviour
{
    public enum Axis { X, Y, Z }

    [Header("Tilt Settings")]
    public Axis tiltAxis = Axis.X;   // Which axis to rotate on
    public float maxAngle = 20f;     // How far it tilts each way
    public float speed = 2f;         // How fast it tilts

    void Update()
    {
        float tilt = Mathf.Sin(Time.time * speed) * maxAngle;

        Vector3 rotation = transform.localEulerAngles;

        switch (tiltAxis)
        {
            case Axis.X:
                rotation = new Vector3(tilt, rotation.y, rotation.z);
                break;

            case Axis.Y:
                rotation = new Vector3(rotation.x, tilt, rotation.z);
                break;

            case Axis.Z:
                rotation = new Vector3(rotation.x, rotation.y, tilt);
                break;
        }

        transform.localRotation = Quaternion.Euler(rotation);
    }
}
