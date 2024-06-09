using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
 

public class AnimationSheet2 : MonoBehaviour
{
   [SerializeField] Animator anim;

    PlayerInput PlayerControls;

    int walk;
    int ability;
    int jump;

    private void Awake()
    {
        PlayerControls = new PlayerInput();
        
        PlayerControls.Gameplay1.Movement.preformed += ctx => Debug.Log(ctx.ReadValueAsObject());

    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        walk = Animator.StringToHash("Walk");
        ability = Animator.StringToHash("Ability");
        jump = Animator.StringToHash("Jump");
    }

    void Movement ()
    {
        bool walk1 = anim.GetBool(walk);
        bool jump1 = anim.GetBool(jump);
        bool ability1 = anim.GetBool(ability);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
