using UnityEngine;
using System.Collections;

public class EnableJumpingTip : MonoBehaviour
{
    public GameObject tipsUiObject;  
    public float delay = 5f;

    private void OnEnable()
    {
        // Subscribe to the event
        PlayerCollision.OnPlayerCollisionWithJumpTipBox += HandlePlayerCollision;
    }

    private void OnDisable()
    {
        // Unsubscribe from the event
        PlayerCollision.OnPlayerCollisionWithJumpTipBox -= HandlePlayerCollision;
    }

    private void HandlePlayerCollision()
    {
        // Start the coroutine to enable the tips object after a delay
        StartCoroutine(EnableTipsAfterDelay(delay));
    }

    private IEnumerator EnableTipsAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Enable the tips object
        tipsUiObject.SetActive(true);
    }
}
