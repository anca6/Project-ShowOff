using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : MonoBehaviour
{

    // The Player whose z-axis movement is tracked
    public GameObject playerObject; 
    public RectTransform uiObject; // The UI object that will move up and down

    public float LevelStartZcoordinate = -60f;
    public float LevelEndZcoordinate = 800f;
    public float UIMinY = -150f;
    public float UIMaxY = 150f;

    void Start()
    {
        if (playerObject == null || uiObject == null)
        {
            Debug.LogError("Not all objects assigned");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float currentZPosition = playerObject.transform.position.z;

        // Map the z position to the y range
        float mappedYPosition = Map(currentZPosition, LevelStartZcoordinate, LevelEndZcoordinate, UIMinY, UIMaxY);

        //Prevent it from going below set Y
        if (mappedYPosition < UIMinY)
        {
            mappedYPosition = UIMinY;
        }

        // Set the y position of the UI object
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, mappedYPosition);

    }

    float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
