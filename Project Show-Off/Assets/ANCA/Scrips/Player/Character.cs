using UnityEngine;
using UnityEngine.InputSystem;

public class Character : PlayerMovement
{
    protected PlayerInput playerInput;
    protected InputAction moveAction;
    private InputAction jumpAction;
  //  private InputAction lookAction;


    //properties for player movement
    [Header("Movement")]
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float acceleration = 10f;
    [SerializeField] protected private float rotationSpeed = 5f;
    [SerializeField] protected private Transform orientation;
    [SerializeField] protected float grVelocityAmplifier; //ground velocity amplifier

    //properties for player jumping
    [Header("Jump Properties")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float jumpCooldown;
    protected bool canJump = true;

    protected virtual void Awake()
    {
        playerInput = GetComponentInParent<PlayerInput>();

        moveAction = playerInput.actions["Movement"];
        jumpAction = playerInput.actions["Jump"];
       // lookAction = playerInput.actions["Look"];

        jumpAction.performed += ctx => Jump();
    }
    private void OnEnable()
    {
         moveAction.Enable();
        jumpAction.Enable();
        //lookAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        //lookAction.Disable();
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


    //player movement
    protected override void Movement()
    {
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (IsGrounded())
        {
            rb.velocity *= grVelocityAmplifier;
        }


        Vector2 input = moveAction.ReadValue<Vector3>();

        float horizontalInput = input.x;
        float verticalInput = input.y;


        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDir != Vector3.zero)
            rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

        rb.velocity += movementDir * acceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

    }
    protected virtual void Jump()
    {
        if (IsGrounded() && canJump) //if the player is not mid-air and can jump
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            canJump = false;
            Invoke(nameof(ResetJump), jumpCooldown);

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
        return Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround);
    }

}
