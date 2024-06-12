using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingPlatform : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction abilityAction;

    //properties for moving platform mechanic
    [SerializeField] private List<Transform> platformWaypoints;
    [SerializeField] private float movingSpeed;
    [SerializeField] private int allowedCharacter;
    [SerializeField] private GameObject playerObj;

    private int targetWaypointIndex;

    private Transform targetWaypoint;
    private Transform previousWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    private PlayerSwitch playerSwitch;

    private bool onPlatform;

    private void Awake()
    {
        playerInput = playerObj.GetComponent<PlayerInput>();

        abilityAction = playerInput.actions["Ability"];
        abilityAction.performed += ctx => GoToNextWaypoint();
        
    }
    private void OnEnable()
    {
        abilityAction.Enable();
        playerSwitch = FindObjectOfType<PlayerSwitch>();
    }

    private void OnDisable()
    {
       abilityAction.Disable();
    }

    //setting up the first waypoint target in the list
    private void Start()
    {
        if (platformWaypoints == null || platformWaypoints.Count == 0)
        {
            Debug.LogError("platformWaypoints is null or empty.");
            return;
        }

        Debug.Log("number of waypoints: " + platformWaypoints.Count);

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
        if (playerSwitch == null || playerSwitch.currentCharacter != allowedCharacter) return; //if the current character is not the character with this mechanic

        elapsedTime += Time.deltaTime;

        //smoothly moving and rotating to the target waypoint, with acceleration/deceleration at the start/end
        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0, 1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);
    }

    //calculates distance between previous waypoint and next waypoint
    private void GoToNextWaypoint()
    {
        if (!onPlatform /*|| playerSwitch == null || playerSwitch.currentCharacter != allowedCharacter*/)
        {

            Debug.Log("something wrong here");
            return;//if the current character is not the character with this mechanic
        }
        //return; //if the current character is not the character with this mechanic


            previousWaypoint = GetWaypointIndex(targetWaypointIndex);
            targetWaypointIndex = GetNextWaypointIndex(targetWaypointIndex);
            targetWaypoint = GetWaypointIndex(targetWaypointIndex);

            elapsedTime = 0;

            float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
            timeToWaypoint = distanceToWaypoint / movingSpeed;

        ///sara sound ability here
        //FindObjectOfType<AudioManager>().Play("Sara's ability");
    }

    //parenting the player gameobject to the platform so it applies the position & rotation of the platform
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
            onPlatform = true;
        }
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            onPlatform = false;
            other.transform.SetParent(null);
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
