using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime;
    private bool isRunning;

    void Start()
    {
        // Initialize the timer and start it
        elapsedTime = 0f;
        isRunning = true;
    }

    void Update()
    {
        if (isRunning)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Format the time as minutes:seconds and update the TextMeshPro text
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime % 60F);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 100F) % 100F);

            timerText.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, milliseconds);
        }
    }

    // Call this method to stop the timer
    public void StopTimer()
    {
        isRunning = false;
    }

    // Call this method to start the timer
    public void StartTimer()
    {
        isRunning = true;
    }

    // Call this method to reset the timer
    public void ResetTimer()
    {
        elapsedTime = 0f;
        timerText.text = "00:00:00";
    }
}
