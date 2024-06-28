using System.Collections;
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
        //wait for the break delay
        yield return new WaitForSeconds(breakDelay);

        //disabling the platform
        platformCollider.enabled = false;
        platformRenderer.enabled = false;

        breakingVFX.Play();

        //wait for the respawn time
        yield return new WaitForSeconds(respawnTime);

        //re-enable the platform
        platformCollider.enabled = true;
        platformRenderer.enabled = true;
    }
}
