using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObjects;
    [SerializeField] private InputActionAsset actionAsset; //reference to the input action asset

    private List<PlayerInput> players = new List<PlayerInput>(); //list of players: player 1 and player 2

    private void Start()
    {
        for (int i = 0; i < playerObjects.Count; i++)
        {
            ActivatePlayer(i);
        }
    }

    private void ActivatePlayer(int index)
    {
        if (index >= playerObjects.Count)
        {
            Debug.LogWarning("Not enough player objects");
            return;
        }

        var playerObject = playerObjects[index];
        playerObject.SetActive(true);

        var playerInput = playerObject.GetComponent<PlayerInput>();

        if (Gamepad.all.Count > index)
        {
            AssignGamepad(playerInput, index);
        }
        else
        {
            AssignKeyboard(playerInput, index);
        }

        players.Add(playerInput);
        playerObject.name = "Player" + (index + 1);
        Debug.Log("Added: " + playerObject.name);
    }

    private void AssignGamepad(PlayerInput playerInput, int index)
    {
        if (Gamepad.all.Count > index)
        {
            var gamepad = Gamepad.all[index];
            InputUser.PerformPairingWithDevice(gamepad, playerInput.user);
            playerInput.SwitchCurrentControlScheme("Gamepad", gamepad);
        }
        else
        {
            Debug.LogWarning($"No gamepad found for player {index + 1}");
            AssignKeyboard(playerInput, index); //fallback to keyboard control
        }
    }

    private void AssignKeyboard(PlayerInput playerInput, int index)
    {
        if (index == 0 || index == 1)
        {
            playerInput.SwitchCurrentControlScheme("Keyboard", Keyboard.current);
        }
    }
}
