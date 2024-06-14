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

        }
    }
    private void KeyboardInput()
    {
       
    }
}
