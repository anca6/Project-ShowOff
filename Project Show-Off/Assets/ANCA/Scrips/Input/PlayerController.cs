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
            Debug.Log($"{gameObject.name} Gamepad Input Detected");
            if (gamepad.buttonSouth.wasPressedThisFrame)
            {
                //Debug.Log($"{gameObject.name} Ability action triggered");
            }
        }
    }
    private void KeyboardInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            Debug.Log($"{gameObject.name} Keyboard Input Detected");
            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                Debug.Log($"{gameObject.name} Ability action triggered");
                // Ability action logic here
            }
        }
    }
}
