using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    //properties for the projectile mechanic
    [SerializeField] private GameObject projectilePrefab;

    //properties for the left/right hand of the player (where furbie will shoot from)
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [SerializeField] private float shootingRadius = 15f;
    [SerializeField] private LayerMask targetsLayer;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Ability.performed += ctx => ShootProjectile(); //calling the shoot projectile method when the ability button is pressed
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }

    private void OnDisable()
    {
       playerControls.Disable();
    }
    private void ShootProjectile()
    {
        GameObject closestTarget = FindClosestTarget();
        if (closestTarget == null)
        {
            return;
        }
        Transform shootingHand = GetClosestHand(closestTarget.transform.position);

        //instantiating a new projectile gameobject from the closest hand to the target
        GameObject projectile = Instantiate(projectilePrefab, shootingHand.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        Vector3 directionToTarget = (closestTarget.transform.position - shootingHand.position).normalized;
        projectileScript.SetTargetDirection(directionToTarget);

        Target target = closestTarget.GetComponent<Target>();
        if (target != null)
        {
            target.MarkAsHit(); //setting the target to isHit so we don't try to shoot at it again
        }
    }
    private GameObject FindClosestTarget()
    {
        //creating a list of all the colliders from the targets layer 
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRadius, targetsLayer);
        GameObject closest = null; //initializing the closest gameobject
        float closestDistance = float.MaxValue; //creating maximum value for distance comparison

        foreach (Collider hitCollider in hitColliders)
        {
            Target target = hitCollider.gameObject.GetComponent<Target>(); //getting the target script from all gameobjects in the layer

            if (target != null && !target.IsHit) //checking if the target exists and has not been hit before
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closest = hitCollider.gameObject;
                    closestDistance = distance; //sets the closest distance to the last closest distance
                }
            }
        }

        return closest;
    }

    //returns the transform component of the closest hand in relation to the target object
    private Transform GetClosestHand(Vector3 targetPosition)
    {
        float distanceLeftHand = Vector3.Distance(leftHand.position, targetPosition);
        float distanceRightHand = Vector3.Distance(rightHand.position, targetPosition);

        return distanceLeftHand < distanceRightHand ? leftHand : rightHand;
    }

}
