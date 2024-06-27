using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private PlayerInput playerInput;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (playerInput.currentControlScheme == "Gamepad")
        {
            GamepadInput();
        }
        else if (playerInput.currentControlScheme == "Keyboard")
        {
            KeyboardInput();
        }
    }

    private void GamepadInput()
    {
        var gamepad = playerInput.devices[0] as Gamepad;
        if (gamepad != null)
        {
            Debug.Log($"{gameObject.name} gamepad input");
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
            }
        }
    }
    private void KeyboardInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            Debug.Log($"{gameObject.name} keyboard input");
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                Debug.Log($"{gameObject.name} ability action triggered");
            }
        }
    }
}
