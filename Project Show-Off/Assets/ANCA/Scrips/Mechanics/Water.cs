using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Water : MonoBehaviour
{
    [SerializeField] private float waterSpeed;
   
    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Character character = collision.gameObject.GetComponent<Character>();
            if(character != null)
            {
                character.IncreaseSpeed(waterSpeed);
            }

        }
    }
    private void OnCollisonExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Character character = collision.gameObject.GetComponent<Character>();
            if (character != null)
            {
                character.DecreaseSpeed(waterSpeed);
            }

        }
    }
}
