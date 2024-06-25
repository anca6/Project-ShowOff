using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jojo : Character
{
    [Header("Boost Properties")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] float minBreakSpeed = 10;
    [SerializeField] private float explosionDelay = 0.5f;

    //bool StandupFinished = true;
    public AudioClip clip2;
   /* protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
    }
*/
    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on the parent gameObject");
        }

        if (CharacterAnimator != null) return;
        Animator animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("No animator found assigned");
            return;
        }
        CharacterAnimator = animator;
    }


    /*void StandupStraight()
    {
        //rb.MoveRotation(Quaternion.identity); // maybe this works?
        if (StandupFinished && !Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Jojo stands up"); // TODO: work with state machine
            StartCoroutine(StandupRoutine());
        }
    }*/


    /* IEnumerator StandupRoutine()
     {
         StandupFinished = false;
         Quaternion startRotation = transform.rotation;
         Vector3 lookDir = rb.velocity.magnitude > 0 ? rb.velocity.normalized : Vector3.forward;

         for (int i=0;i<=20;i++) // TODO: Maybe make framerate independent: use delta time
         {
             //Debug.Log("Delta time: " + Time.deltaTime);
             rb.MoveRotation(Quaternion.Slerp(startRotation, Quaternion.LookRotation(lookDir), i / 20f));

             yield return new WaitForEndOfFrame();
         }
         StandupFinished = true;
     }
 */
    /* protected override void Movement()
     {
         //TODO: Add rolling state machine here

         rb.constraints = RigidbodyConstraints.None;

         if (IsGrounded())
         {
             rb.velocity *= grVelocityAmplifier;


            *//* if (rb.velocity.magnitude<0.5f) // now hard coded.. // TODO: state machine. Only do this code when currently rolling
             {
                 StandupStraight();
             }*//*
         }

         Vector2 input = moveAction.ReadValue<Vector3>();

         float horizontalInput = input.x;
         float verticalInput = input.y;

 *//*        float horizontalInput = playerControls.Gameplay.Movement.ReadValue<Vector3>().x;
         float verticalInput = playerControls.Gameplay.Movement.ReadValue<Vector3>().y;
 *//*
         //moving in the forward direction of the player
         Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

         //if (movementDir != Vector3.zero)

             //rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

         rb.velocity += movementDir * acceleration;
         rb.velocity = Vector3.ClampMagnitude(rb.velocity, moveSpeed);

     }*/
    protected override void Movement()
    {
        rb.constraints = RigidbodyConstraints.None;

        isGrounded = IsGrounded();
        if (isGrounded) rb.velocity *= grVelocityAmplifier;

        Vector3 input = moveAction.ReadValue<Vector3>();

        float horizontalInput = input.x;
        float verticalInput = input.y;

        //moving in the forward direction of the player
        Vector3 movementDir = orientation.forward * verticalInput + orientation.right * horizontalInput;

        isMoving = movementDir != Vector3.zero;
        if (isMoving)
            rb.transform.forward = Vector3.Slerp(rb.transform.forward, movementDir.normalized, Time.deltaTime * rotationSpeed); //rotate the player parent directly, not the individual character children

        Vector3 horizontalVelocity = movementDir * moveSpeed;

        rb.velocity = new Vector3(horizontalVelocity.x, rb.velocity.y, horizontalVelocity.z);
    }

    public void HandleCollision(Collision collision)
    {
        if (gameObject.activeInHierarchy)
        {

            if (collision.gameObject.CompareTag("BreakObj"))
            {
                // vector projection: how hard do we hit the wall?

                float impactSpeed = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity));
                Debug.Log("Speed on impact: " + impactSpeed + " normal: " + collision.contacts[0].normal + " velocity: " + collision.relativeVelocity);
                if (impactSpeed > minBreakSpeed)
                {
                    StartCoroutine(DelayedExplosion(collision.gameObject));
                }
                //Destroy(explosionEffect);
            }
        }
    }

    private IEnumerator DelayedExplosion(GameObject breakableObject)
    {
        yield return new WaitForSeconds(explosionDelay);
        TriggerExplosion(transform.position);
        Destroy(breakableObject);
        source.PlayOneShot(clip);
    }


    private void TriggerExplosion(Vector3 position)
    {
        Instantiate(explosionEffect, position, Quaternion.identity);
    }

}