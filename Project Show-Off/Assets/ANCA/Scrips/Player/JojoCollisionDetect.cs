using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JojoCollisionDetect : MonoBehaviour
{
    [SerializeField] private Jojo jojoCollisionHandler;

    private void Start()
    {
        if (jojoCollisionHandler == null)
            Debug.Log("Jojo object not assigned");
    }

    private void OnCollisionEnter(Collision collision)
    {
       jojoCollisionHandler.HandleCollision(collision);
    }
}
