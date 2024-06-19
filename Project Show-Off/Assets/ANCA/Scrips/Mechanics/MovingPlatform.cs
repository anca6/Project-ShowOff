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

    private void Awake()
    {
        foreach (GameObject player in players)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];
            abilityAction.performed += ctx => TryGoToNextWaypoint(playerInput);
            actionList.Add(abilityAction);
        }
    }

    private void OnEnable()
    {
        if (actionList != null)
        {
            foreach (InputAction action in actionList)
            {
                action.Enable();
            }
        }
    }

    private void OnDisable()
    {
        if (actionList != null)
        {
            foreach (InputAction action in actionList)
            {
                action.Disable();
            }
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

        return currentCharacterIndex == allowedCharacter;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            currentPlayerInput = other.gameObject.GetComponent<PlayerInput>();
            onPlatform = IsAllowedCharacter(currentPlayerInput);
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
