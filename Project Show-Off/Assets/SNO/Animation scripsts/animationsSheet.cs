using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.InputSystem;

public class animationsSheet : MonoBehaviour
{


    [SerializeField] private Animator sarahAnim;
    [SerializeField] protected LayerMask isGround;
    [SerializeField] protected float playerHeight;
    [SerializeField] private bool groundOverwrite = false;

    [SerializeField] private PlayerControls _actions;
    private bool moving = false;
    private bool grounded;
    private bool ability;
    private bool jump;


    private void Awake()
    {
        _actions = new PlayerControls();
        //_actions.Gameplay1.Movement.performed += ctx => moving = ctx.ReadValueAsButton();

        // _actions.Gameplay1.Movement.canceled += ctx => moving = ctx.ReadValueAsButton();
        
          _actions.Gameplay1.Ability.performed += ctx => ability = ctx.ReadValueAsButton();
          _actions.Gameplay1.Ability.canceled += ctx => ability = ctx.ReadValueAsButton();
        /*
          _actions.Gameplay1.Jump.performed += ctx => jump = ctx.ReadValueAsButton();
          _actions.Gameplay1.Jump.canceled += ctx => jump = ctx.ReadValueAsButton();
        */


    }

    private void OnEnable()
    {
        _actions.Gameplay1.Movement.Enable();
        _actions.Gameplay1.Ability.Enable();
        _actions.Gameplay1.Jump.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {


        //if no object is dragged in warn and return
        if (sarahAnim == null)
        {
            Debug.LogWarning("Sarah Animator not set on:" + gameObject);
            return;
        }
        //if playerheight is below 0 warn
        if (playerHeight < 0)
        {
            Debug.LogWarning("PlayerHeight <0 " + gameObject + "will forever fall");
        }


        sarahAnim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {


        //if the moving bool is true then sett walk to true in the animator controller
        if (moving)

        {
            sarahAnim.SetBool("Walk", true);
            Debug.Log(gameObject + "Walk set to:" + moving);
        }
        else if (!moving)
        {
            sarahAnim.SetBool("Walk", false);
            //Debug.Log(gameObject + "Walk set to:" + moving);
        }

        //if ability is true then sett ability to true in the animator controller
        if (ability)
        {
            sarahAnim.SetBool("Ability", true);

        }
        else sarahAnim.SetBool("Ability", false);

        //if jump is true and grounded is true sett jump in the animator contorller to true
        if (jump && grounded)
        {

            sarahAnim.SetBool("Jump", true);

            Debug.Log(gameObject + "Jump set to: true");
        }
        else sarahAnim.SetBool("Jump", false);

        //if the player is not grounded set float to true in the animator controller
        if (!grounded)
        {
            sarahAnim.SetBool("Float", true);
        }
        else
        {
            sarahAnim.SetBool("Float", false);
        }



        //check if the player is grounded or not
        if (Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround) && groundOverwrite == false)
        {
            grounded = true;
        }
        else
        {
            grounded = false;

            //if they're not grounded draw a ray same lenght as the one wich checks for ground.
            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f), Color.red);
        }


    }



}
