using UnityEngine;
using UnityEngine.UI;

public class SpeedrunTimer : MonoBehaviour
{
    public Text timerText;          // Reference to UI Text for displaying time
    private float elapsedTime = 0f;
    private bool isRunning = false;

    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            UpdateTimerUI();
        }
    }

    void UpdateTimerUI()
    {
        int minutes = Mathf.FloorToInt(elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(elapsedTime % 60f);
        int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);

        timerText.text = $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }
}
