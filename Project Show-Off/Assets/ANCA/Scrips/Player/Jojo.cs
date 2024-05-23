using UnityEngine;

public class Jojo : Character
{
    //properties for ball movement
    [Header("Ball Properties")]
    [SerializeField] private float ballSpeed = 10f;
    [SerializeField] private float ballAcceleration = 15f;

    protected override void Movement()
    {
        rb.constraints = RigidbodyConstraints.None; //remove constrains to simulate better rolling

        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        if (movementDir != Vector3.zero)
        {
            //transform.forward = Vector3.Slerp(transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed);
        }

        rb.velocity += movementDir * ballAcceleration;
        //rb.velocity = Vector3.ClampMagnitude(rb.velocity, ballSpeed);

        //can add base.Movement(); after the rigid body constrains?
        //

        //Debug.Log("jojo script");
    }
}

