using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakablePlatform : MonoBehaviour
{
    [SerializeField] private float breakDelay;
    [SerializeField] private float respawnTime;

    [SerializeField] private ParticleSystem breakingVFX;

    private Collider platformCollider;
    private Renderer platformRenderer;

    private void Start()
    {
        platformCollider = GetComponent<Collider>();
        platformRenderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartCoroutine(BreakPlatform());
        }
    }

    private IEnumerator BreakPlatform()
    {
        // Wait for the specified break delay
        yield return new WaitForSeconds(breakDelay);

        // Disable the platform by disabling the Collider and Renderer
        platformCollider.enabled = false;
        platformRenderer.enabled = false;

        breakingVFX.Play();

        // Wait for the specified respawn time
        yield return new WaitForSeconds(respawnTime);

        // Re-enable the platform by enabling the Collider and Renderer
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
    }
}
