using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    public SpeedrunTimer timerScript;  // Assign in Inspector
    public bool isStartTrigger = true; // Is this the start or end trigger?

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (isStartTrigger)
            {
                timerScript.StartTimer();
                Debug.Log("Timer Started");
            }
            else
            {
                timerScript.StopTimer();
                Debug.Log("Timer Stopped");
            }
        }
    }
}