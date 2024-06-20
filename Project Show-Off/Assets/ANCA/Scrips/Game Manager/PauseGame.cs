using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private GameObject pausePanel;

    public void Pause()
    {
        pausePanel.SetActive(true);
    }
}
