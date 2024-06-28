using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class Jojo : Character
{
    [Header("Boost Properties")]
    [SerializeField] private GameObject explosionEffect;
    [SerializeField] float minBreakSpeed = 10;
    [SerializeField] private float explosionDelay = 0.5f;

    public AudioClip clip2;


    protected override void Awake()
    {
        base.Awake();
    }
    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("rigidbody component not found on parent gameObject");
        }
        rb.velocity = Vector3.zero;

        if (CharacterAnimator != null) return;
        Animator animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogWarning("no animator assigned");
            return;
        }
        CharacterAnimator = animator;
    }

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
                float impactSpeed = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity));
                if (impactSpeed > minBreakSpeed)
                {
                    StartCoroutine(DelayedExplosion(collision.gameObject));
                }
            }


            if (collision.gameObject.CompareTag("BreakWall"))
            {
                float impactSpeed = Mathf.Abs(Vector3.Dot(collision.contacts[0].normal, collision.relativeVelocity));
                if (impactSpeed > minBreakSpeed)
                {
                    source.PlayOneShot(clip2);
                    Destructable destructable = collision.gameObject.GetComponent<Destructable>();
                    destructable.DestroyWall();
                }
            }
        }
    }

    private IEnumerator DelayedExplosion(GameObject breakableObject)
    {
        yield return new WaitForSeconds(explosionDelay);
        TriggerExplosion(new Vector3(breakableObject.transform.position.x, breakableObject.transform.position.y + 1f, breakableObject.transform.position.z));
        Destroy(breakableObject);
        source.PlayOneShot(clip2);
    }


    private void TriggerExplosion(Vector3 position)
    {
        Instantiate(explosionEffect, position, Quaternion.identity);
    }

}