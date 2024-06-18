using UnityEngine;
using UnityEngine.InputSystem;
public class Furbie : Character
{
    // The InputAction for the ability input
    private InputAction abilityAction;
    
    //properties for player jumping & dashing
    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashTimer;
    public AudioSource source;
    public AudioClip clip;
    
    // Ability animation trigger flag
    protected bool abilityTriggered;
    
    // Cache the hash of the Ability bool for performance reasons
    private static readonly int Ability = Animator.StringToHash("Ability");
    
    protected override void Awake(){
        // Call base class Awake method first
        base.Awake();
        // Chain our own InputAction assignment onto it
        abilityAction = playerInput.actions["Ability"];
    }
    
    // Enable the InputAction when needed
    private void OnEnable() => abilityAction.Enable();
    
    // Disable the InputAction when needed
    private void OnDisable() => abilityAction.Disable();

    protected override void FixedUpdate()
    {
        base.FixedUpdate();

        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
        }
    }
    protected override void Jump()
    {
        if (dashTimer > 0)
            return;

        dashTimer = dashCooldown;

        //if the player is not mid-air and can jump
        if (IsGrounded() && canJump) 
        {
            Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce;

            delayedForceToApply = forceToApply;

            //add a delayed dash force for better simulation
            Invoke(nameof(DelayedDashForce), 0.025f);
            Invoke(nameof(ResetJump), dashDuration);

            ///furbie jump sound here
            source.PlayOneShot(clip);
        }
    }
    
    // Override UpdateInternalStates in order to chain our own extra check onto it
    protected override void UpdateInternalStates(){
        // Call the base class method we're overriding
        base.UpdateInternalStates();
        // Check if the ability InputAction is triggered
        abilityTriggered = Mathf.Approximately(abilityAction.ReadValue<float>(), 1);
    }
    
    // Override UpdateAnimationParameters to chain our own animation bool assignment onto it
    protected override void UpdateAnimationParameters(){
        // Call the base class method we're overriding
        base.UpdateAnimationParameters();
        // Set the ability Animator boolean
        CharacterAnimator.SetBool(Ability, abilityTriggered);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        canJump = false;
    }

}
