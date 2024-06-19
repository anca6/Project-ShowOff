using UnityEngine;
using UnityEngine.InputSystem;

public class Character : PlayerMovement
{
    protected PlayerInput playerInput;
    protected InputAction moveAction;
    private InputAction jumpAction;
    private InputAction lookAction;

   
    public bool canCollect = true;

    //properties for player movement
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float acceleration = 10f;
    [SerializeField] protected float rotationSpeed = 5f;
    [SerializeField] protected Transform orientation;
    [SerializeField] protected float grVelocityAmplifier; //ground velocity amplifier

    private float originalMoveSpeed;
    private float originalAcceleration;
    private float originalRotationSpeed;

    //properties for player jumping
    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    protected bool canJump = true;

    // Custom gravity properties
    [Header("Gravity Properties")]
    [SerializeField] private float gravityScale = 1f;

    protected virtual void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
        lookAction = playerInput.actions["Look"];

        jumpAction.performed += ctx => Jump();

        originalMoveSpeed = moveSpeed;
        originalAcceleration = acceleration;
        originalRotationSpeed = rotationSpeed;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        lookAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        lookAction.Disable();
    }

    //getting the rigid body component of the "Player" parent
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the parent gameObject");
        }
    }

    protected override void FixedUpdate()
    {
        ApplyGravity();
        Movement();
    }

    //player movement
    protected override void Movement()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (IsGrounded())
        {
            rb.velocity *= grVelocityAmplifier;
        }

        Vector3 input = moveAction.ReadValue<Vector3>();

        float horizontalInput = input.x;
        float verticalInput = input.y;

        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDir != Vector3.zero)
            rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

        Vector3 horizontalVelocity = movementDir * moveSpeed;

        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }
    
    private void ApplyGravity()
    {
        if (!IsGrounded())
        {
            rb.velocity += Vector3.down * gravityScale * Time.deltaTime;
        }
    }

    protected virtual void Jump()
    {
        if (IsGrounded() && canJump) //if the player is not mid-air and can jump
        {
            rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z); // Reset vertical velocity before jump
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);

            EnableJumpingTip.CharacterJumpNotifier.NotifyCharacterJump();

            Debug.Log("character jump");



            ///jump sound here
            //FindObjectOfType<AudioManager>().Play("jump");
        }
    }

    protected void ResetJump()
    {
        canJump = true;
    }

    //checking if the player is on the ground
    protected bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.1f, isGround);
    }

    //


    // Method to modify the speed
    public void IncreaseSpeed(float speedModifier)
    {
        moveSpeed += speedModifier;
    }

    // Method to reset the speed
    public void DecreaseSpeed(float speedModifier)
    {
        moveSpeed -= speedModifier;
    }
    public void SetSpeed(float speed)
    {
        moveSpeed = speed;
        acceleration = 0; 
    }
    public void ResetSpeed()
    {
        moveSpeed = originalMoveSpeed;
        acceleration = originalAcceleration;
        rotationSpeed = originalRotationSpeed;
    }

     // Method to check if the player can collect
    public bool CanCollect()
    {
        return canCollect;
    }

    // Method to set the collect state
    public void SetCollectState(bool state)
    {
        canCollect = state;
    }
}
