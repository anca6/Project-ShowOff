using UnityEngine;

public class TipMovement : MonoBehaviour
{
    public GameObject MonitoredPlayer;
    public GameObject Tip; 
    public float stationaryTime = 5.0f;
    public float movementThreshold = 5.0f;

    private Vector3 initialPosition;
    private float timer;

    void Start()
    {
        if (MonitoredPlayer == null || Tip == null)
        {
            Debug.LogError("Assign both objects in the inspector. - MovementTip");
            enabled = false;
            return;
        }

        initialPosition = MonitoredPlayer.transform.position;
        timer = 0.0f;
        Tip.SetActive(false);
    }

    void Update()
    {
        // Calculate the distance moved since the initial position
        float distanceMoved = Vector3.Distance(MonitoredPlayer.transform.position, initialPosition);

        // Check if the target object has moved less than the threshold
        if (distanceMoved < movementThreshold)
        {
            // Increase the timer if the movement is within the threshold
            timer += Time.deltaTime;

            // Check if the object has had limited movement for the specified time
            if (timer >= stationaryTime)
            {
                Tip.SetActive(true);
            }
        }
        else
        {
            // Reset the timer if the movement exceeds the threshold
            timer = 0.0f;
            initialPosition = MonitoredPlayer.transform.position;
            Tip.SetActive(false); // Optionally disable the object again if movement resumes
        }
    }
}
