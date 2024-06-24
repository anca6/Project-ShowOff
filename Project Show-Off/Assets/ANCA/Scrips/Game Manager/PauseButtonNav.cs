using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseButtonNav : MonoBehaviour
{
    [SerializeField] private Button[] buttons;
    [SerializeField] private GameObject[] hoverImages;

    private int currentIndex = 0;

    private void Start()
    {
        UpdateButtonSelection();
    }

    private void Update()
    {
        if (Gamepad.current == null) return;

        if (Gamepad.current.dpad.up.wasPressedThisFrame || Gamepad.current.leftStick.up.wasPressedThisFrame)
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = buttons.Length - 1;
            UpdateButtonSelection();
        }

        if (Gamepad.current.dpad.down.wasPressedThisFrame || Gamepad.current.leftStick.down.wasPressedThisFrame)
        {
            currentIndex++;
            if (currentIndex >= buttons.Length) currentIndex = 0;
            UpdateButtonSelection();
        }

        if (Gamepad.current.buttonSouth.wasPressedThisFrame) //select button
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    private void UpdateButtonSelection()
    {
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);
    }
}
