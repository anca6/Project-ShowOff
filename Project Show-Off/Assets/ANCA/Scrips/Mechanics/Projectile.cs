using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    //properties for the projectile movement
    [SerializeField] private float speed = 10f;
    private Vector3 targetDirection;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void SetTargetDirection(Vector3 direction)
    {
        targetDirection = direction;
    }

    //move after the target direction
    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.MarkAsHit(); //colliding with the target makes the target invisible to the player
            MeshRenderer targetRenderer = other.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.yellow; //turn the target mesh colour to yellow
            }
            Destroy(gameObject); //destroy the projectile on collision with target
        }
    }
}
