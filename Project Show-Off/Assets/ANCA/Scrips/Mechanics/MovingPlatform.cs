using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
<<<<<<< HEAD
=======
using UnityEngine.Rendering.Universal.Internal;
>>>>>>> engineer
using UnityEngine.Windows;

public class MovingPlatform : MonoBehaviour
{
<<<<<<< HEAD
    private PlayerInput playerInput;
    private InputAction abilityAction;
=======
    //private PlayerInput playerInput;
    //private InputAction abilityAction;
>>>>>>> engineer

    //properties for moving platform mechanic
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

    // private PlayerInput currentPlayerInput;

    private void Awake()
    {
        /*playerInput = playerObj.GetComponent<PlayerInput>();

        abilityAction = playerInput.actions["Ability"];
        abilityAction.performed += ctx => GoToNextWaypoint();*/
<<<<<<< HEAD
        
        foreach(GameObject player in players)
=======

        foreach (GameObject player in players)
>>>>>>> engineer
        {
            var playerInput = player.GetComponent<PlayerInput>();
            var abilityAction = playerInput.actions["Ability"];
            abilityAction.performed += ctx => GoToNextWaypoint(playerInput);
<<<<<<< HEAD
=======
            actionList.Add(abilityAction);
>>>>>>> engineer
        }
    }
    private void OnEnable()
    {
<<<<<<< HEAD
        abilityAction.Enable();
=======
        foreach (InputAction action in actionList)
        {
            action.Enable();
        }
>>>>>>> engineer
    }

    private void OnDisable()
    {
<<<<<<< HEAD
       abilityAction.Disable();
=======
        foreach (InputAction action in actionList)
        {
            action.Disable();
        }
>>>>>>> engineer
    }

    //setting up the first waypoint target in the list
    private void Start()
    {
        if (platformWaypoints == null || platformWaypoints.Count == 0)
        {
            Debug.LogError("platformWaypoints is null or empty.");
            return;
        }

        //Debug.Log("number of waypoints: " + platformWaypoints.Count);

        targetWaypointIndex = 0;
        targetWaypoint = platformWaypoints[targetWaypointIndex];
        previousWaypoint = targetWaypoint;
    }

    private void FixedUpdate()
    {
        if(onPlatform)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
<<<<<<< HEAD
        if (!IsAllowedCharacter(currentPlayerInput)) return; //if the current character is not the character with this mechanic
=======
        //if (!IsAllowedCharacter(currentPlayerInput)) return; //if the current character is not the character with this mechanic
>>>>>>> engineer

        elapsedTime += Time.deltaTime;

        //smoothly moving and rotating to the target waypoint, with acceleration/deceleration at the start/end
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
    }

    //calculates distance between previous waypoint and next waypoint
    private void GoToNextWaypoint(PlayerInput input)
    {
<<<<<<< HEAD
        /* if (!onPlatform *//*|| playerSwitch == null || playerSwitch.currentCharacter != allowedCharacter*//*)
         {

             Debug.Log("something wrong here");
             return;//if the current character is not the character with this mechanic
         }*/
        //return; //if the current character is not the character with this mechanic

        if (!onPlatform || currentPlayerInput != input || !IsAllowedCharacter(input))
=======
        if (/*!onPlatform ||*//* currentPlayerInput != input ||*/ IsAllowedCharacter(input) == false) //return;
>>>>>>> engineer
        {
            Debug.Log("something wrong here");
            return;
        }


        previousWaypoint = GetWaypointIndex(targetWaypointIndex);
            targetWaypointIndex = GetNextWaypointIndex(targetWaypointIndex);
            targetWaypoint = GetWaypointIndex(targetWaypointIndex);

            elapsedTime = 0;

<<<<<<< HEAD
            float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
            timeToWaypoint = distanceToWaypoint / movingSpeed;
=======
        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / movingSpeed;
>>>>>>> engineer

        ///sara sound ability here
        //FindObjectOfType<AudioManager>().Play("Sara's ability");
    }
    private bool IsAllowedCharacter(PlayerInput input)
    {
        if (input == null) return false;

        int player1CharacterIndex = GameManager.instance.GetPlayer1CharacterIndex();
        int player2CharacterIndex = GameManager.instance.GetPlayer2CharacterIndex();

        //get the index of the current player character
        int currentCharacterIndex = -1;
        for (int i = 0; i < players.Count; i++)
        {
            if (players[i].GetComponent<PlayerInput>() == input)
            {
                currentCharacterIndex = i == 0 ? player1CharacterIndex : player2CharacterIndex;
                break;
            }
        }

        return currentCharacterIndex == allowedCharacter;
    }

    //parenting the player gameobject to the platform so it applies the position & rotation of the platform
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            onPlatform = true;
<<<<<<< HEAD
            currentPlayerInput = other.gameObject.GetComponent<PlayerInput>();
=======
            //currentPlayerInput = other.gameObject.GetComponent<PlayerInput>();
>>>>>>> engineer
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
            other.transform.SetParent(null);
            //currentPlayerInput = null;
        }
    }

    //return the transform component of x waypoint
    private Transform GetWaypointIndex(int waypointIndex)
    {
        return platformWaypoints[waypointIndex].transform;
    }

    //return the index of the next waypoint
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
