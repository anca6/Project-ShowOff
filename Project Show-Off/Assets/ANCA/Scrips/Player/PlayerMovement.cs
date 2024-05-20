using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] protected private float rotationSpeed = 5f;
    [SerializeField] protected private Transform orientation;

    [Header("Ground Properties")]
    [SerializeField] private LayerMask isGround;
    [SerializeField] private float playerHeight;
    private bool grounded;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    private bool canJump = true;

    protected private Rigidbody rb;
    private enum MovementMode { Default, Jojo_ball }
    private MovementMode currentMode = MovementMode.Default;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Jump.performed += ctx => Jump();
        //playerControls.Gameplay.Ability.performed += ctx => SwitchMovement();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
     protected virtual void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        //player movement
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (movementDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed);

        DefaultMovement(movementDir);

        /* switch (currentMode)
         {
             case MovementMode.Default:
                 Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

                 if (movementDir != Vector3.zero)
                     transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed);
                 DefaultMovement(movementDir);
                 rb.constraints = RigidbodyConstraints.FreezeRotation;
                 break;
             case MovementMode.Jojo_ball:
                 Vector3 ballDir = orientation.forward * verticalInput;

                 if (ballDir != Vector3.zero)
                     transform.forward = Vector3.Slerp(transform.forward, ballDir.normalized, Time.deltaTime * rotationSpeed);
                 BallMovement(ballDir);
                 rb.constraints = RigidbodyConstraints.None;
                 break;
         }*/
        Debug.Log("player script");
    }

    private void DefaultMovement(Vector3 movementDir)
    {
        rb.velocity += movementDir * acceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
    }
    /*private void BallMovement(Vector3 movementDir)
    {
        // Apply force to move the ball
        Vector3 force = movementDir * ballAcceleration * Time.fixedDeltaTime;
        rb.AddForce(force, ForceMode.Acceleration);

        // Apply torque to simulate rolling
        Vector3 torque = new Vector3(movementDir.z, 0, -movementDir.x) * ballTorque;
        rb.AddTorque(torque, ForceMode.Force);

        rb.velocity = Vector3.ClampMagnitude(rb.velocity, ballSpeed);
    }*/

    protected virtual void Jump()
    {
        if (IsGrounded() && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);
        }
    }

    private void ResetJump()
    {
        canJump = true;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround);
    }

    /*private void SwitchMovement()
    {
        if (currentMode == MovementMode.Default)
        {
            currentMode = MovementMode.Jojo_ball;
            Debug.Log("switched to ball");
            rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        }
        else
        {
            currentMode = MovementMode.Default;
            Debug.Log("back to walking");
            rb.constraints = RigidbodyConstraints.FreezeRotation;
        }
    }*/

}

