using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public SpeedrunTimer timerScript;  // Assign in Inspector
    public bool isStartTrigger = true; // Is this the start or end trigger?

    private static bool hasStarted = false; // Tracks if the timer has started

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isStartTrigger)
            {
                if (!hasStarted)
                {
                    timerScript.StartTimer();
                    hasStarted = true;
                    Debug.Log("Timer Started");
                }
            }
            else
            {
                timerScript.StopTimer();
                Debug.Log("Timer Stopped");
            }
        }
    }
}
