using UnityEngine;
using UnityEngine.InputSystem;
public class Furbie : Character
{
    
    private InputAction abilityAction;
    
    //properties for player jumping & dashing
    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashTimer;
   

    protected bool abilityTriggered;
    
    private static readonly int Ability = Animator.StringToHash("Ability");
    
    protected override void Awake(){
        base.Awake();
        abilityAction = playerInput.actions["Ability"];
    }

    private void OnEnable() => abilityAction.Enable();

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

            source.PlayOneShot(clip);
        }
    }

    protected override void UpdateInternalStates(){
        base.UpdateInternalStates();
        abilityTriggered = Mathf.Approximately(abilityAction.ReadValue<float>(), 1);
    }

    protected override void UpdateAnimationParameters(){
        base.UpdateAnimationParameters();
        CharacterAnimator.SetBool(Ability, abilityTriggered);
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        canJump = false;
    }

}
