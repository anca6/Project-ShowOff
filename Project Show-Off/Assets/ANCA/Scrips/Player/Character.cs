using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : PlayerMovement
{
    protected PlayerControls playerControls;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] protected private float rotationSpeed = 5f;
    [SerializeField] protected private Transform orientation;

    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    protected bool canJump = true;
    protected virtual void Awake()
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

    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    protected override void Movement()
    {
        //player movement
        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (movementDir != Vector3.zero)
            transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed);

        rb.velocity += movementDir * acceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

    }
    protected virtual void Jump()
    {
        if (IsGrounded() && canJump)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);

            Debug.Log("character jump");
        }
    }
    protected void ResetJump()
    {
        canJump = true;
    }
    protected bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround);
    }

}
