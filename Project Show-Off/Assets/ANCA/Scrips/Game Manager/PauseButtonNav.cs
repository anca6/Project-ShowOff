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
        if (buttons == null || buttons.Length == 0)
        {
            Debug.LogError("No buttons assigned.");
            return;
        }

        if (hoverImages == null || hoverImages.Length != buttons.Length)
        {
            Debug.LogError("Hover images array must match the buttons array length.");
            return;
        }

        UpdateButtonSelection();
    }

    private void Update()
    {
        if (Gamepad.current == null)
        {
            Debug.LogWarning("No current gamepad found.");
            return;
        }

        bool moved = false;

        if (Gamepad.current.dpad.up.wasPressedThisFrame || Gamepad.current.leftStick.up.wasPressedThisFrame)
        {
            currentIndex--;
            if (currentIndex < 0) currentIndex = buttons.Length - 1;
            moved = true;
        }

        if (Gamepad.current.dpad.down.wasPressedThisFrame || Gamepad.current.leftStick.down.wasPressedThisFrame)
        {
            currentIndex++;
            if (currentIndex >= buttons.Length) currentIndex = 0;
            moved = true;
        }

        if (moved)
        {
            UpdateButtonSelection();
        }

        if (Gamepad.current.buttonNorth.wasPressedThisFrame) //select button
        {
            buttons[currentIndex].onClick.Invoke();
        }
    }

    private void UpdateButtonSelection()
    {
        EventSystem.current.SetSelectedGameObject(buttons[currentIndex].gameObject);

        for (int i = 0; i < hoverImages.Length; i++)
        {
            hoverImages[i].SetActive(i == currentIndex);
        }
    }
}
