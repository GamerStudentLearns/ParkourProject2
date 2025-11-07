using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SpeedrunTimer : MonoBehaviour
{
    public Text timerText;          // Reference to UI Text for displaying time
    private float elapsedTime = 0f;
    private bool isRunning = false;
    public static float finalTime = 0f;

   



    public void StartTimer()
    {
        elapsedTime = 0f;
        isRunning = true;
    }


    public void StopTimer()
    {
        isRunning = false;
        finalTime = elapsedTime;
        Debug.Log($"Speedrun ended at: {FormatTime(finalTime)}");
       
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
    string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60f);
        int seconds = Mathf.FloorToInt(time % 60f);
        int milliseconds = Mathf.FloorToInt((time * 1000f) % 1000f);
        return $"{minutes:00}:{seconds:00}.{milliseconds:000}";
    }
  





}
