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
            return;
        }

        if (hoverImages == null || hoverImages.Length != buttons.Length)
        {
            return;
        }

        UpdateButtonSelection();
    }

    private void Update()
    {
        if (Gamepad.current == null)
        {
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
            if (buttons[currentIndex].gameObject.activeInHierarchy)
            {
                buttons[currentIndex].onClick.Invoke();
            } else
            {
                Debug.Log("Nope");
            }
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
