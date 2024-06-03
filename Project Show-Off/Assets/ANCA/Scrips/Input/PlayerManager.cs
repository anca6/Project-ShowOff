using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Users;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> playerObjects;

    private List<PlayerInput>  players = new List<PlayerInput>();

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
            var gamepad = Gamepad.all[index];
            InputUser.PerformPairingWithDevice(gamepad, playerInput.user);
        }

        players.Add(playerInput);

        playerObject.name = "Player" + (index + 1);

    }
}
