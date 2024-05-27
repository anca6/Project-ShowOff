using UnityEngine;

public class Character : PlayerMovement
{
    //reference to player controls input manager
    protected PlayerControls playerControls;

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
        rb.constraints = RigidbodyConstraints.FreezeRotation;

        if (IsGrounded())
        {
            rb.velocity *= grVelocityAmplifier;
        }

        float horizontalinput = playerControls.Gameplay.Movement.ReadValue<Vector3>().x;
        float verticalInput = playerControls.Gameplay.Movement.ReadValue<Vector3>().y;

        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        if (movementDir != Vector3.zero)
            rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

        rb.velocity += movementDir * acceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);
       // Debug.Log(rb.velocity);

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
            FindObjectOfType<AudioManager>().Play("jump");
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
