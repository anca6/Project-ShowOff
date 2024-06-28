using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;

    private bool isPaused = false;

    private void Start()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (Gamepad.current != null)
        {
            Debug.Log("gamepad connected: " + Gamepad.current);
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                Debug.Log("start button pressed");
                TogglePause();
            }
        }
        else
        {
            Debug.Log("no gamepad connected");
        }


    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //pausing the time in game
        pauseButton.interactable = false;
        isPaused = true;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; //continuing the game
        pauseButton.interactable = true;
        isPaused = false;
    }

    private void TogglePause()
    {
        if (isPaused)
        {
            Continue();
        }
        else
        {
            Pause();
        }
    }
}
