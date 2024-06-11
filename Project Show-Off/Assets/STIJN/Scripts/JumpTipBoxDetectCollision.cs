using UnityEngine;
using System;

public class JumpTipBoxDetectionCollision : MonoBehaviour
{
    public static event Action OnPlayerCollisionWithJumpTipBox;

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the Player
        if (other.CompareTag("Player"))
        {
            Debug.Log("JumpTip Trigger Hit!");
            OnPlayerCollisionWithJumpTipBox?.Invoke();
        }
    }
}
