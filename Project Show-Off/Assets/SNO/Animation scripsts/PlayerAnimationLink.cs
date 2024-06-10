using UnityEngine;
public class PlayerAnimationLink : MonoBehaviour
{
    

    [SerializeField] private Animator Anim;
    [SerializeField] protected LayerMask isGround;
    [SerializeField] protected float playerHeight;
    [SerializeField] private bool groundOverwrite = false;

    private bool moving = false;
    private bool Grounded;
    // Start is called before the first frame update
    void Start()
    {
      
       
        //if the animator is not detected send a warning
        if (Anim == null)
        {
            Debug.LogWarning("Sarah Animator not set on:" + gameObject);
            return;
        }
        //if the playerheight is bellow 0 tell the user the character will only float
        if (playerHeight < 0)
        {
            Debug.LogWarning("PlayerHeight <0 " + gameObject + "will forever fall");
        }

       //get the animator from the entity
        Anim = GetComponent<Animator>();

    }


    // Update is called once per frame
    void Update()
    {

        // if the player presses W,A,S,D then this means the player is moving.
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



      
        //if w,a,s or d is pressed toggle the "Walk" bool in the animator to on
        if (moving)
  
        {
            Anim.SetBool("Walk", true);
            Debug.Log(gameObject + "Walk set to:" + moving);
        }
        //toggle it off
        else if (!moving)
        {
            Anim.SetBool("Walk", false);
            //Debug.Log(gameObject + "Walk set to:" + moving);
        }

        // if hte player presses F toggle teh "ability" bool in the animator on
        if(Input.GetKey(KeyCode.F))
        {
            Anim.SetBool("Ability", true) ;
            Debug.Log(gameObject + "Ability set to: true");
        }
        //toggle it off
        else Anim.SetBool("Ability", false) ;

        //if space is pressed and the entity is grounded putt the jump bool to true
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {

            Anim.SetBool("Jump", true);

            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f));
            Debug.Log(gameObject + "Jump set to: true");
        }
        //putt it to off
        else Anim.SetBool("Jump", false) ;

        //if the entity is not grounded toggle the floating bool int he animator to on
        if (!Grounded)
        {
            Anim.SetBool("Float", true);
        }
        //toggle it off
        else
        {
            Anim.SetBool("Float", false);
        }


        
        //check if the entity is half the playerHeight above an object with a collider wich is also on the layer isGround.
        //if it is set the bool grounded to true
        //we do this to detect if the player can jump and/or should be floating.
        if ( Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, playerHeight * 0.5f + 0.3f, isGround) && groundOverwrite == false)
      {
            Grounded = true;
        }
        //if not sett it to false and vissualize the ray
        else
        {
            Grounded = false;
            Debug.DrawRay(transform.position, Vector3.down * (playerHeight * 0.5f + 0.3f), Color.red);

        }
      
      // Debug.Log("Grounded sett to:" + Grounded);
    }
  
    
   
}
