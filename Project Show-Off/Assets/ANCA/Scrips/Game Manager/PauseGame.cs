using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private Button pauseButton;

    private void Update()
    {
        if (Gamepad.current != null)
        {
            Debug.Log("Gamepad connected: " + Gamepad.current);
            if (Gamepad.current.startButton.wasPressedThisFrame)
            {
                Debug.Log("Start button pressed");
                Pause();
            }
        }
        else
        {
            Debug.Log("No gamepad connected");
        }
    }

    public void Pause()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        pauseButton.interactable = false;
    }

    public void Continue()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        pauseButton.interactable = true;
    }
}
