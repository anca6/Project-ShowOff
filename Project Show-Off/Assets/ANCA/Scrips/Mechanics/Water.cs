using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float waterSpeed = 2.0f;

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character character = collision.gameObject.GetComponentInChildren<Character>();
            if (character != null)
            {
                character.SetSpeed(waterSpeed);
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character character = collision.gameObject.GetComponentInChildren<Character>();
            if (character != null)
            {
                character.ResetSpeed();
            }
        }
    }
}
