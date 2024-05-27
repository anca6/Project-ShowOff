using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jojo : Character
{
    [Header("Boost Properties")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float maxSpeedDuration = 3f;
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] float minBreakSpeed = 10;



    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }

    protected override void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Jump.performed += ctx => Jump();
    }
 
    void StandupStraight()
    {
        //rb.MoveRotation(Quaternion.identity); // maybe this works?
        Debug.Log("Jojo stands up");
        StartCoroutine(StandupRoutine());
    }

    IEnumerator StandupRoutine()
    {
        Quaternion startRotation = transform.rotation;
        for (int i=0;i<=20;i++) // TODO: Maybe make framerate independent
        {
            rb.MoveRotation(Quaternion.Slerp(startRotation, Quaternion.identity, i / 20f));
            yield return new WaitForEndOfFrame();
        }
    }

    protected override void Movement()
    {
        rb.constraints = RigidbodyConstraints.None;

        if (IsGrounded())
        {
            rb.velocity *= grVelocityAmplifier;

            if (rb.velocity.magnitude<0.5f) // now hard coded..
            {
                StandupStraight();
            }
        }

        float horizontalinput = playerControls.Gameplay.Movement.ReadValue<Vector3>().x;
        float verticalInput = playerControls.Gameplay.Movement.ReadValue<Vector3>().y;

        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalinput;

        //if (movementDir != Vector3.zero)
            //rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

        rb.velocity += movementDir * acceleration;
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

        //check for momentum


    }

    public void HandleCollision(Collision collision)
    {
        if (collision.gameObject.CompareTag("X"))
        {
            // vector projection: how hard do we hit the wall?

            float impactSpeed = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity));
            Debug.Log("Speed on impact: " + impactSpeed+" normal: "+ collision.contacts[0].normal + " velocity: "+ collision.relativeVelocity);
            if (impactSpeed > minBreakSpeed)
            {

                TriggerExplosion(transform.position);
                Destroy(collision.gameObject);
            }
            //Destroy(explosionEffect);

            //TO DO: add coroutine to delay the explosion effect

            //TO DO: delay the wall getting destroyed
        }
    }

    private void TriggerExplosion(Vector3 position)
    {
        Instantiate(explosionEffect, position, Quaternion.identity);
    }

}