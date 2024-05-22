using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{

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

