using UnityEngine;
using UnityEngine.UI;

public class ObjectMover : MonoBehaviour
{
    public GameObject referenceObject; // The object whose z-axis movement is tracked
    public RectTransform uiObject; // The UI object that will move up and down

    public float LevelStartZcoordinate = -60f;
    public float LevelEndZcoordinate = 800f;
    public float UIMinY = -150f;
    public float UIMaxY = 150f;

    void Start()
    {
        if (referenceObject == null || uiObject == null)
        {
            Debug.LogError("Please assign the referenceObject and uiObject in the inspector.");
            enabled = false;
            return;
        }
    }

    void Update()
    {
        float currentZPosition = referenceObject.transform.position.z;

        // Map the z position to the y range
        float mappedYPosition = Map(currentZPosition, LevelStartZcoordinate, LevelEndZcoordinate, UIMinY, UIMaxY);

        // Set the y position of the UI object
        uiObject.anchoredPosition = new Vector2(uiObject.anchoredPosition.x, mappedYPosition);
    }

    float Map(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        return (value - fromMin) / (fromMax - fromMin) * (toMax - toMin) + toMin;
    }
}
