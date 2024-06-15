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
            return;
            /*// Process gamepad input here
            Vector3 move = gamepad.leftStick.ReadValue();
            // Add gamepad-specific processing logic here*/
        }
    }
    private void KeyboardInput()
    {
        var keyboard = Keyboard.current;
        if (keyboard != null)
        {
            return;
        }
    }
}
