using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform leftHand;
    [SerializeField] private Transform rightHand;

    [SerializeField] private float shootingRadius = 15f;
    [SerializeField] private LayerMask targetsLayer;

    private void Update()
    {
        if (Input.GetButtonUp("Fire")){
            ShootProjectile();
        }
    }
    private void ShootProjectile()
    {
        GameObject closestTarget = FindClosestTarget();
        if(closestTarget != null)
        {
            return;
        }
        Transform shootingHand = GetClosestHand(closestTarget.transform.position);

        GameObject projectile = Instantiate(projectilePrefab, shootingHand.position, Quaternion.identity);
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        Vector3 directionToTarget = (closestTarget.transform.position - shootingHand.position).normalized;  
        projectileScript.SetTargetDirection(directionToTarget);
    }
    private GameObject FindClosestTarget()
    {
        GameObject closest = null;
        return closest;
    }
    private Transform GetClosestHand(Vector3 targetPosition)
    {
        Transform target = null;
        return target;
    }

}
