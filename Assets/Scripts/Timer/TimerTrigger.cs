using UnityEngine;
using UnityEngine.SceneManagement;

public class TimerTrigger : MonoBehaviour
{
    public SpeedrunTimer timerScript;  // Assign in Inspector
    public bool isStartTrigger = true; // Is this the start or end trigger?

    private static bool hasStarted = false; // Tracks if the timer has started

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasStarted = false; // Reset timer state on scene load
    }

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
                hasStarted = false; // Allow timer to be started again
                Debug.Log("Timer Stopped");
            }
        }
    }
}
