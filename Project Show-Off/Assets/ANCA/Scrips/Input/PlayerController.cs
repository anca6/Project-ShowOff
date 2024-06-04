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
            var gamepad = playerInput.devices[0] as Gamepad;
            if (gamepad != null)
            {
                // This is just to ensure the correct gamepad is assigned.
                // Your movement and action scripts should be separate and handle the input.
            }
        }
    }
}
