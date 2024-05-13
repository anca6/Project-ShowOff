using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private Transform orientation;

    [Header("Ground Check")]
    private float playerHeight;
    bool grounded;
    [SerializeField] public LayerMask isGround;
    [SerializeField] private float groundDrag;

    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    void Update()
    {
        //player movement
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;
        //rb.AddForce(movementDir.normalized * moveSpeed, ForceMode.Force);

        rb.velocity += movementDir * acceleration /** Time.deltaTime*/;

        //limit the velocity to the maximum move speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

        //SpeedControl();

        //ground check
        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, isGround);

        if (grounded)
            rb.drag = groundDrag;
        else rb.drag = 0;
    }

    /*private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }*/

}

