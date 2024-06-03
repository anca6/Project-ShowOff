using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private List<Transform> spawnPoints;
    [SerializeField] private GameObject playerPrefab;

    private PlayerInputManager playerInputManager;

    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.onPlayerJoined += OnPlayerJoined;
        playerInputManager.onPlayerLeft += OnPlayerLeft;
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log("player Joined: " + playerInput.playerIndex);

        for(int i = 0; i < spawnPoints.Count; i++)
        {
            Instantiate(playerPrefab, spawnPoints[i].transform.position, Quaternion.identity);
        }

       // playerInput.transform.position = new Vector3(playerInput.playerIndex * 2.0f, 0, 0);

    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log("player Left: " + playerInput.playerIndex);
    }
}
