using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Furbie : Character
{
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

        if (IsGrounded() && canJump)
        {
            Vector3 forceToApply = transform.forward * dashForce + transform.up * dashUpwardForce;

            delayedForceToApply = forceToApply;

            Invoke(nameof(DelayedDashForce), 0.025f);
            Invoke(nameof(ResetJump), dashDuration);
        }
    }

    private Vector3 delayedForceToApply;

    private void DelayedDashForce()
    {
        rb.AddForce(delayedForceToApply, ForceMode.Impulse);
        canJump = false;
    }
}
