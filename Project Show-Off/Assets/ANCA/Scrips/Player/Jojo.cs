using UnityEngine;

public class Jojo : Character
{
    [Header("Boost Properties")]
    [SerializeField] private float maxSpeed = 20f;
    [SerializeField] private float maxSpeedDuration = 3f;
    [SerializeField] private GameObject explosionEffect;
    private float currentSpeed;
    private bool isMaxSpeed = false;
    private float maxSpeedTimer = 0f;

    //detecting double press
/*    private float lastBoostPressTime = 0f;
    private float doublePressThreshold = 0.5f;*/

    protected override void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        currentSpeed = moveSpeed;
    }

    protected override void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Jump.performed += ctx => Jump();
        playerControls.Gameplay.Boost.performed += ctx => OnBoostPressed();
    }
 
    protected override void Movement()
    {
        base.Movement();

        if (isMaxSpeed)
        {
            maxSpeedTimer -= Time.deltaTime;
            if (maxSpeedTimer <= 0f)
            {
                isMaxSpeed = false;
                currentSpeed = moveSpeed;
            }
        }

       // Debug.Log(currentSpeed);
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collided with wall");
        if (isMaxSpeed && other.CompareTag("X"))
        {
            TriggerExplosion(other.transform.position);
            Destroy(other.gameObject);
        }
    }

    private void TriggerExplosion(Vector3 position)
    {
        Instantiate(explosionEffect, position, Quaternion.identity);
    }

    protected void Boost()
    {
        isMaxSpeed = true;
        maxSpeedTimer = maxSpeedDuration;
        currentSpeed = maxSpeed;
    }
    private void OnBoostPressed()
    {
        /*float timeSinceLastPress = Time.time - lastBoostPressTime;

        if (timeSinceLastPress <= doublePressThreshold)
        {
            Debug.Log("pressed boost");
            Boost();
        }
        lastBoostPressTime = Time.time;*/
        Debug.Log("double tap");
        Boost();
    }

}