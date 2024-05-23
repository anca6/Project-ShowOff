using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    //properties for checking ground collision
    [Header("Ground Properties")]
    [SerializeField] protected LayerMask isGround;
    [SerializeField] protected float playerHeight;
    protected bool grounded;

    protected Rigidbody rb;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody>();
        
    }
    protected abstract void Movement();
    
     protected virtual void FixedUpdate()
    {
        Movement();
    }


}

