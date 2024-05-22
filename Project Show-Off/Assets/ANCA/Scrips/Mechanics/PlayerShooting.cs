using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [SerializeField] private float shootingRadius = 15f;
    [SerializeField] private LayerMask targetsLayer;

    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Gameplay.Ability.performed += ctx => ShootProjectile();
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

        GameObject projectile = Instantiate(projectilePrefab, shootingHand.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        Vector3 directionToTarget = (closestTarget.transform.position - shootingHand.position).normalized;
        projectileScript.SetTargetDirection(directionToTarget);

        Target target = closestTarget.GetComponent<Target>();
        if (target != null)
        {
            target.MarkAsHit();
        }
    }
    private GameObject FindClosestTarget()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, shootingRadius, targetsLayer);
        GameObject closest = null;
        float closestDistance = float.MaxValue;

        foreach (Collider hitCollider in hitColliders)
        {
            Target target = hitCollider.gameObject.GetComponent<Target>();
            if (target != null && !target.IsHit)
            {
                float distance = Vector3.Distance(transform.position, hitCollider.transform.position);
                if (distance < closestDistance)
                {
                    closest = hitCollider.gameObject;
                    closestDistance = distance;
                }
            }
        }

        return closest;
    }
    private Transform GetClosestHand(Vector3 targetPosition)
    {
        float distanceLeftHand = Vector3.Distance(leftHand.position, targetPosition);
        float distanceRightHand = Vector3.Distance(rightHand.position, targetPosition);

        return distanceLeftHand < distanceRightHand ? leftHand : rightHand;
    }

}
