using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class markMovement : MonoBehaviour
{
    public Animator kratosAnimator;

    private KratosActions _actions;

    private Vector2 _currentMovement;
    private bool isRunning;
    private bool isAttacking;
    private bool isJumping;

    private Transform camTransform;

    private void Awake()
    {
        _actions = new KratosActions();
        _actions.PC.Move.performed += ctx => _currentMovement = ctx.ReadValue<Vector2>();
        _actions.PC.Run.performed += ctx => isRunning = ctx.ReadValueAsButton();
        _actions.PC.Run.canceled += ctx => isRunning = ctx.ReadValueAsButton();
        _actions.PC.Attack.performed += ctx => isAttacking = ctx.ReadValueAsButton();
        _actions.PC.Attack.canceled += ctx => isAttacking = ctx.ReadValueAsButton();
        _actions.PC.Jump.performed += ctx => isJumping = ctx.ReadValueAsButton();
        _actions.PC.Jump.canceled += ctx => isJumping = ctx.ReadValueAsButton();


        camTransform = Camera.main.transform;
    }  

    private void OnEnable()
    {
        _actions.PC.Move.Enable();
        _actions.PC.Run.Enable();
        _actions.PC.Attack.Enable();
        _actions.PC.Jump.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        HandleRotation();

        if(_currentMovement.y != 0)
        {
            kratosAnimator.SetBool("isWalking", true);
        }
        else
        {
            kratosAnimator.SetBool("isWalking", false);
        }
        if(isRunning)
        {
            kratosAnimator.SetBool("isRunning", true);
        }
        else
        {
            kratosAnimator.SetBool("isRunning", false);
        }
        if (isAttacking)
        {
            kratosAnimator.SetBool("isAttacking", true);
        }
        else
        {
            kratosAnimator.SetBool("isAttacking", false);
        }
        if (isJumping)
        {
            kratosAnimator.SetBool("isJumping", true);
        }
        else
        {
            kratosAnimator.SetBool("isJumping", false);
        }
    }

    void HandleRotation()
    {
        float targetAngle = camTransform.eulerAngles.y;
        Quaternion rot = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Lerp(transform.rotation, rot, Time.deltaTime * 5f);
    }

}
