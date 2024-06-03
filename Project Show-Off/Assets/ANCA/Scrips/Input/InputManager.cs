using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeft;
    }

     void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("player Joined: " + playerInput.playerIndex);

        playerInput.transform.position = new Vector3(playerInput.playerIndex * 2.0f, 0, 0);
    }

     void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("player Left: " + playerInput.playerIndex);
    }
}
