using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : MonoBehaviour
{
    [Header("Platform properties")]
    [SerializeField] private List<Transform> platformWaypoints;
    [SerializeField] private List<GameObject> players;
    [SerializeField] private float movingSpeed;
    [SerializeField] private int allowedCharacter;

    [Header("Sound")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    [Header("VFX")]
    [SerializeField] private ParticleSystem petalsVFX;


    private int targetWaypointIndex;
    private Transform targetWaypoint;
    private Transform previousWaypoint;
    private float timeToWaypoint;
    private float elapsedTime;
    private bool onPlatform;
    private PlayerInput currentPlayerInput;

    private Dictionary<PlayerInput, InputAction> playerInputActions;

    private void Awake()
    {
        playerInputActions = new Dictionary<PlayerInput, InputAction>();

        foreach (GameObject player in players)
        {
            var playerInput = player.GetComponent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];

            abilityAction.performed += ctx => TryGoToNextWaypoint(playerInput);
            playerInputActions.Add(playerInput, abilityAction);
        }
    }

    private void OnEnable()
    {
        foreach (var entry in playerInputActions)
        {
            entry.Value.Enable();
        }
    }

    private void OnDisable()
    {
        foreach (var entry in playerInputActions)
        {
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

    private void Update()
    {
        GamepadInput();
    }
    private void FixedUpdate()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);

        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

        source.PlayOneShot(clip);
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
        if (elapsedTime < timeToWaypoint)
        {
            Debug.Log("Sorry still moving. Please wait");

        }
        else
        {
            Debug.Log("Moving to next waypoint");
            GoToNextWaypoint();
        }
    }

    private void GoToNextWaypoint()
    {
        previousWaypoint = GetWaypointTransform(targetWaypointIndex);
        targetWaypointIndex = GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = GetWaypointTransform(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / movingSpeed;

        petalsVFX.Play();

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

        return currentCharacterIndex == allowedCharacter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("should be on platform");
            collision.transform.SetParent(transform);
            var newPlayerInput = collision.gameObject.GetComponent<PlayerInput>();
            if (newPlayerInput!=null && IsAllowedCharacter(newPlayerInput)) // two Saras: still bug - didnt fix
            {
                Debug.Log("Sara has entered the platform");
                currentPlayerInput = newPlayerInput;
                onPlatform = currentPlayerInput != null;
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
            if (currentPlayerInput != null)
            {
                if (currentPlayerInput == collision.gameObject.GetComponent<PlayerInput>())
                {
                    Debug.Log("No one on platform");
                    currentPlayerInput = null;
                    onPlatform = false;
                }
            }
        }
    }

    private Transform GetWaypointTransform(int waypointIndex)
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

    private void GamepadInput()
    {
        foreach (var entry in playerInputActions)
        {
            if (entry.Key.devices.Count > 0)
            {
                var gamepad = entry.Key.devices[0] as Gamepad;
                if (gamepad != null && gamepad.buttonSouth.wasPressedThisFrame)
                {
                    string cpname = " ? ";
                    if (currentPlayerInput!=null)
                    {
                        cpname = currentPlayerInput.gameObject.name;
                    }
                    Debug.Log("South button pressed on gamepad for player: " + entry.Key.gameObject.name+ " currentPlayerInput is "+cpname);

                    //check if the player with this gamepad is on the platform and allowed character
                    if (entry.Key == currentPlayerInput && IsAllowedCharacter(currentPlayerInput))
                    {
                        TryGoToNextWaypoint(entry.Key);
                    }
                }
            }
            else
            {
                Debug.LogWarning($"No devices found for player: {entry.Key.gameObject.name}");
            }
        }
    }
}
