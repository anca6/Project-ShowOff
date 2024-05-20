using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Jojo : MonoBehaviour
{
    [Header("Ball Properties")]
    [SerializeField] private float ballSpeed = 10f;
    [SerializeField] private float ballAcceleration = 15f;
    [SerializeField] private float ballTorque = 15f;

    [SerializeField] private Transform orientation;
    private float rotationSpeed = 5f;
    [SerializeField] private PlayerMovement playerMovement;

    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        playerMovement = GetComponentInParent<PlayerMovement>();

        playerMovement.enabled = false;
        rb.constraints = RigidbodyConstraints.None;
    }
    void FixedUpdate()
    {
        Movement();
    }
    void Movement()

    {
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDir != Vector3.zero)
        {
            transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed);
        }

        rb.velocity += movementDir * ballAcceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, ballSpeed);
       

        Debug.Log("jojo script");
    }



}

