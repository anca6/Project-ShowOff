using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
    private void FixedUpdate()
    {
        //transform.position += targetDirection * speed * Time.deltaTime;
        rb.MovePosition(rb.position + targetDirection * speed * Time.fixedDeltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("collider entered trigger: " + other.name);
        Target target = other.GetComponent<Target>();
        if (target != null)
        {
            target.MarkAsHit();
            MeshRenderer targetRenderer = other.GetComponent<MeshRenderer>();
            if (targetRenderer != null)
            {
                targetRenderer.material.color = Color.yellow;
            }
            Destroy(gameObject);
        }
    }
}
