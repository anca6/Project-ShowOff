using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;
using UnityEngine.InputSystem;

public class Animation : MonoBehaviour
{
    

    [SerializeField] private Animator sarahAnim;
    [SerializeField] protected LayerMask isGround;
    [SerializeField] protected float playerHeight;
    [SerializeField] private bool groundOverwrite = false;

    private bool moving = false;
    private bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
      
       

        if (sarahAnim == null)
        {
            Debug.LogWarning("Sarah Animator not set on:" + gameObject);
            return;
        }
        if (playerHeight < 0)
        {
            Debug.LogWarning("PlayerHeight <0 " + gameObject + "will forever fall");
        }

       
        sarahAnim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.A))
        {
            moving = true;

        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            moving = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            moving = true;
        }
        else if (Input.GetKey(KeyCode.W))
        {
            moving = true;
        }

        if (Input.GetKey(KeyCode.A) == false &&(Input.GetKey(KeyCode.D) == false) && (Input.GetKey(KeyCode.S) == false) && Input.GetKey(KeyCode.W) == false)
        {
            moving = false;
        }



      

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

        if(Input.GetKey(KeyCode.F))
        {
            sarahAnim.SetBool("Ability", true) ;
            Debug.Log(gameObject + "Ability set to: true");
        }
        else sarahAnim.SetBool("Ability", false) ;

        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {

            sarahAnim.SetBool("Jump", true);

            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f));
            Debug.Log(gameObject + "Jump set to: true");
        }
        else sarahAnim.SetBool("Jump", false) ;

        if (!Grounded)
        {
            sarahAnim.SetBool("Float", true);
        }
        else
        {
            sarahAnim.SetBool("Float", false);
        }


        

        if ( Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround) && groundOverwrite == false)
      {
            Grounded = true;
        }
        else
        {
            Grounded = false;

        }
      
       // Debug.Log("Grounded sett to:" + Grounded);
    }
  
    
   
}
