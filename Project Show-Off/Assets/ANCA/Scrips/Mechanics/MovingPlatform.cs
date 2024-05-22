using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    //properties for moving platform mechanic
    [SerializeField] private List<Transform> platformWaypoints;
    [SerializeField] private float movingSpeed;
    [SerializeField] private int allowedCharacter;

    private int targetWaypointIndex;

    private Transform targetWaypoint;
    private Transform previousWaypoint;

    private float timeToWaypoint;
    private float elapsedTime;

    private PlayerControls playerControls;
    private PlayerSwitch playerSwitch;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Ability.performed += ctx => GoToNextWaypoint(); //calls the go to next waypoint method when the ability button is pressed
    }
    private void OnEnable()
    {
        playerControls.Enable();
        playerSwitch = FindObjectOfType<PlayerSwitch>();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Start()
    {
        targetWaypointIndex = 0;
        targetWaypoint = platformWaypoints[targetWaypointIndex];
        previousWaypoint = targetWaypoint;
    }

    private void FixedUpdate()
    {
        if (playerSwitch == null || playerSwitch.currentCharacter != allowedCharacter) return;

        elapsedTime += Time.deltaTime;

        float elapsedPercentage = elapsedTime / timeToWaypoint;
        elapsedPercentage = Mathf.SmoothStep(0 ,1, elapsedPercentage);
        transform.position = Vector3.Lerp(previousWaypoint.position, targetWaypoint.position, elapsedPercentage);
        transform.rotation = Quaternion.Lerp(previousWaypoint.rotation, targetWaypoint.rotation, elapsedPercentage);

    }
    private void GoToNextWaypoint()
    {
        if (playerSwitch == null || playerSwitch.currentCharacter != allowedCharacter) return;

        previousWaypoint = GetWaypointIndex(targetWaypointIndex);
        targetWaypointIndex = GetNextWaypointIndex(targetWaypointIndex);
        targetWaypoint = GetWaypointIndex(targetWaypointIndex);

        elapsedTime = 0;

        float distanceToWaypoint = Vector3.Distance(previousWaypoint.position, targetWaypoint.position);
        timeToWaypoint = distanceToWaypoint / movingSpeed;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }

    private Transform GetWaypointIndex(int waypointIndex)
    {
        return platformWaypoints[waypointIndex].transform;
    }

    private int GetNextWaypointIndex(int currentWaypointIndex)
    {
        int nextWaypointIndex = currentWaypointIndex + 1;
        if(nextWaypointIndex == platformWaypoints.Count)
        {
            nextWaypointIndex = 0;
        }
        return nextWaypointIndex;   
    }
}
