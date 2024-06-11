using UnityEngine;
using System;
using System.Collections;

public class EnableJumpingTip : MonoBehaviour
{
    public GameObject tipsUiObject;
    public float delay = 5f;
    public float JumpThreshold = 5.0f;

    private Coroutine currentCoroutine;

    private void OnEnable()
    {
        JumpTipBoxDetectionCollision.OnPlayerCollisionWithJumpTipBox += HandlePlayerCollision;
        CharacterJumpNotifier.OnCharacterJump += HandleCharacterJump;
    }

    private void OnDisable()
    {
        JumpTipBoxDetectionCollision.OnPlayerCollisionWithJumpTipBox -= HandlePlayerCollision;
        CharacterJumpNotifier.OnCharacterJump -= HandleCharacterJump;
    }

    private void HandlePlayerCollision()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(EnableTipsAfterDelay(delay));
    }

    private void HandleCharacterJump()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        // disable the tips uit object
        tipsUiObject.SetActive(false);
    }

    private IEnumerator EnableTipsAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Enable the tips ui object
        tipsUiObject.SetActive(true);
    }

    public static class CharacterJumpNotifier
    {
        public static event Action OnCharacterJump;

        public static void NotifyCharacterJump()
        {
            OnCharacterJump?.Invoke();
        }
    }
}