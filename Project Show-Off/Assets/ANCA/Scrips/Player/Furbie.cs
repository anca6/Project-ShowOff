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
    public AudioSource source;
    public AudioClip clip;

    protected bool abilityTriggered;
    
    private static readonly int Ability = Animator.StringToHash("Ability");
    
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
