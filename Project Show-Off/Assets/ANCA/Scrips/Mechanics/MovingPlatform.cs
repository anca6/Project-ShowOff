using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private List<Transform> platformWaypoints;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private float movingSpeed;
    [SerializeField] private int allowedCharacter;

    [SerializeField] private List<InputAction> actionList;

    private int targetWaypointIndex;
    private Transform targetWaypoint;
    private Transform previousWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;
    private bool onPlatform;
    private PlayerInput currentPlayerInput;

    private Dictionary<PlayerInput, InputAction> playerInputActions;

    /*private void Awake()
    {
        foreach (GameObject player in players)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];
            Debug.Log("Setting up ability action for: " + player.name);
            // Explicitly naming the action for debugging purposes

            abilityAction.performed += ctx => TryGoToNextWaypoint(playerInput);


            actionList.Add(abilityAction);
        }
    }*/

    private void Awake()
    {
        playerInputActions = new Dictionary<PlayerInput, InputAction>();

        foreach (GameObject player in players)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];

            Debug.Log("Setting up ability action for: " + player.name);

            abilityAction.performed += ctx => TryGoToNextWaypoint(playerInput);
            playerInputActions.Add(playerInput, abilityAction);
        }
    }

    private void OnEnable()
    {
        foreach (var entry in playerInputActions)
        {
            Debug.Log("Enabling action for player: " + entry.Key.gameObject.name);
            entry.Value.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var entry in playerInputActions)
        {
            Debug.Log("Disabling action for player: " + entry.Key.gameObject.name);
            entry.Value.Disable();
        }
    }

    private void Start()
    {
        if (platformWaypoints == null || platformWaypoints.Count == 0)
        {
            Debug.LogError("platformWaypoints is null or empty.");
            return;
        }

        targetWaypointIndex = 0;
        targetWaypoint = platformWaypoints[targetWaypointIndex];
        previousWaypoint = targetWaypoint;
    }

    private void FixedUpdate()
    {
        if (onPlatform && IsAllowedCharacter(currentPlayerInput))
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
    }

    private void TryGoToNextWaypoint(PlayerInput input)
    {
        Debug.Log("Ability action triggered by: " + input.gameObject.name);

        if (!onPlatform)
        {
            Debug.Log("Not on platform");
            return;
        }

        if (!IsAllowedCharacter(input))
        {
            Debug.Log("Not allowed character");
            return;
        }

        GoToNextWaypoint();
    }

    private void GoToNextWaypoint()
    {
        previousWaypoint = GetWaypointIndex(targetWaypointIndex);
        targetWaypointIndex = GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = GetWaypointIndex(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / movingSpeed;

        Debug.Log("Moving to waypoint: " + targetWaypointIndex);
    }

    private bool IsAllowedCharacter(PlayerInput input)
    {
        if (input == null) return false;

        int player1CharacterIndex = GameManager.instance.GetPlayer1CharacterIndex();
        int player2CharacterIndex = GameManager.instance.GetPlayer2CharacterIndex();

        int currentCharacterIndex = -1;
        for (int i = 0; i < players.Count; i++)
        {
            PlayerInput playerInput = players[i].GetComponent<PlayerInput>();
            if (playerInput == input)
            {
                currentCharacterIndex = (i == 0) ? player1CharacterIndex : player2CharacterIndex;
                break;
            }
        }

        Debug.Log("Player: " + input.gameObject.name + ", Character Index: " + currentCharacterIndex + ", Allowed Character: " + allowedCharacter);
        return currentCharacterIndex == allowedCharacter;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            currentPlayerInput = other.gameObject.GetComponent<PlayerInput>();
            onPlatform = IsAllowedCharacter(currentPlayerInput);

            Debug.Log(other.gameObject.name + " entered platform. OnPlatform: " + onPlatform);
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(null);
            if (currentPlayerInput == other.gameObject.GetComponent<PlayerInput>())
            {
                currentPlayerInput = null;
                onPlatform = false;
            }

            Debug.Log(other.gameObject.name + " exited platform. OnPlatform: " + onPlatform);
        }
    }

    private Transform GetWaypointIndex(int waypointIndex)
    {
        return platformWaypoints[waypointIndex].transform;
    }

    private int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;
        if (nextWaypointIndex == platformWaypoints.Count)
        {
            nextWaypointIndex = 0;
        }
        return nextWaypointIndex;
    }
}
