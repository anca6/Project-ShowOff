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
    [SerializeField] private Transform orientation;

    [Header("Ground Properties")]
    [SerializeField] private LayerMask isGround;
    [SerializeField] private float playerHeight;
    private bool grounded;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    private bool canJump = true;
    private bool isJumping = false;

    Rigidbody rb;
    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Jump.performed += ctx => Jump();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }
    private void FixedUpdate()
    {
        Movement();
    }

    protected virtual void Movement()
    {
        //player movement
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        rb.velocity += movementDir * acceleration /** Time.deltaTime*/;

        //limit the velocity to the maximum move speed
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed + 1);
    }

    protected virtual void Jump()
    {
        if (IsGrounded() && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            isJumping = true;
            Invoke(nameof(ResetJump), jumpCooldown);
        }

    }

    private void ResetJump()
    {
        canJump = true;
        isJumping = false;
    }
    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround);
    }
}

