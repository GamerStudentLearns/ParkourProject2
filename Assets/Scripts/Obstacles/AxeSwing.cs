using UnityEngine;

public class AxeTrapSwing : MonoBehaviour
{
    public float swingSpeed = 1.5f;     // Speed of the swing
    public float swingAngle = 90f;      // Max angle from center
    public float delayBeforeStart = 2f; // Optional delay before swinging starts

    private float startTime;

    void Start()
    {
        startTime = Time.time + delayBeforeStart;
    }

    void Update()
    {
        if (Time.time >= startTime)
        {
            // Pendulum-like swing using sine wave
            float angle = Mathf.Sin((Time.time - startTime) * swingSpeed) * swingAngle;
            transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
