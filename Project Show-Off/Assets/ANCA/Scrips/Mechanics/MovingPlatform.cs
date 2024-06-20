using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : MonoBehaviour
{
    private PlayerInput playerInput;
    private List<InputAction> abilityActions = new List<InputAction>();

    public AudioSource source;
    public AudioClip clip;
    // properties for moving platform mechanic
    [SerializeField] private List<Transform> platformWaypoints;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private float movingSpeed;
    [SerializeField] private int allowedCharacter;

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
            playerInput = player.GetComponentInParent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];
            abilityAction.performed += ctx => GoToNextWaypoint(playerInput);
            abilityActions.Add(abilityAction);
            
        }
    }

    private void OnEnable()
    {
        foreach (var action in abilityActions)
        {
            action.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var action in abilityActions)
        {
            action.Disable();
        }
    }

    // Setting up the first waypoint target in the list
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
        if (onPlatform)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        if (!IsAllowedCharacter(currentPlayerInput)) return;

        elapsedTime += Time.deltaTime;

        // Smoothly moving and rotating to the target waypoint
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
        source.PlayOneShot(clip);
    }

    private void GoToNextWaypoint(PlayerInput input)
    {
        if (!onPlatform)
        {
            Debug.Log("not on platform");
            return;
        }
        if (currentPlayerInput != input)
        {
            Debug.Log("no current player input");
            return;
        }
        if (!IsAllowedCharacter(input))
        {
            Debug.Log("not allowed character");
            return;
        }

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

        // Get the index of the current player character
        int currentCharacterIndex = -1;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponentInParent<PlayerInput>() == input)
            {
                currentCharacterIndex = i == 0 ? player1CharacterIndex : player2CharacterIndex;
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
            onPlatform = true;
            currentPlayerInput = other.gameObject.GetComponentInParent<PlayerInput>();
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
            other.transform.SetParent(null);
            currentPlayerInput = null;
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
