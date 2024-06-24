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

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);
    }
    //move after the target direction

    public void SetTargetDirection(Vector3 direction)
    {
        targetDirection = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Target target = collision.gameObject.GetComponent<Target>();
        if (target != null)
        {
            target.MarkAsHit(); //colliding with the target makes the target invisible to the player
            Destroy(gameObject); //destroy the projectile on collision with target
            
        }
    }
}
