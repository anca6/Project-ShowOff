using UnityEngine;

public class Furbie : Character
{
    //properties for player jumping & dashing
    [Header("Dashing")]
    [SerializeField] private float dashForce;
    [SerializeField] private float dashUpwardForce;
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashCooldown;
    private float dashTimer;

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
            FindObjectOfType<AudioManager>().Play("jump");
        }
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        canJump = false;
    }
}
