using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor.Experimental.GraphView;
using UnityEditor.ShaderGraph;
using UnityEngine;

public abstract class PlayerMovement : MonoBehaviour
{
    private PlayerControls playerControls; //override rb in character start
    //
    [Header("Ground Properties")]
    [SerializeField] protected LayerMask isGround;
    [SerializeField] protected float playerHeight;
    protected bool grounded;

    protected Rigidbody rb;

    private void Awake()
    {
        playerControls = new PlayerControls();
        //playerControls.Gameplay.Jump.performed += ctx => Jump();
        //playerControls.Gameplay.Ability.performed += ctx => SwitchMovement();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
        playerControls.Disable();
    }
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

