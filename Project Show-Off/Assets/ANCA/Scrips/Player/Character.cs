using UnityEngine;

public class Character : PlayerMovement
{
    //reference to player controls input manager
    protected PlayerControls playerControls;

    //properties for player movement
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] protected private float rotationSpeed = 5f;
    [SerializeField] protected private Transform orientation;

    //properties for player jumping
    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    protected bool canJump = true;
    protected virtual void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Jump.performed += ctx => Jump(); //calling the jump method when jump button is pressed
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }

    //getting the rigid body component of the "Player" parent
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    //player movement
    protected override void Movement()
    {
        if (IsGrounded())
        {
            rb.velocity *= 0.95f; // put in inspector
        }

        //freeze the rotation before applying movement
        rb.constraints = RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;

        float horizontalinput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (movementDir != Vector3.zero)
            //transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); // ????

        rb.velocity += movementDir * acceleration;
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

    }
    protected virtual void Jump()
    {
        if (IsGrounded() && canJump) //if the player is not mid-air and can jump
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

    //checking if the player is on the ground
    protected bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround);
    }

}
