using System.Collections;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible properties")]
    [SerializeField] private float speedBuffValue = 1.0f; // Fixed speed buff/debuff value
    [SerializeField] private float duration = 4f;
    [SerializeField] private float pickupCooldown = 8f;
/*
    [Header("Pickup Effects")]
    [SerializeField] private GameObject pickupEffectspeedup;
    [SerializeField] private GameObject pickupEffectslowdown;
*/
    [Header("Sound")]
    [SerializeField] private AudioSource source;
    [SerializeField] private AudioClip clip;

    [SerializeField] private ParticleSystem pinkCloudVFX;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger entered with player.");

            Character character = other.GetComponentInChildren<Character>();
            if (character != null)
            {
                Debug.Log("Character component found.");

                if (character.CanCollect())
                {
                    Debug.Log("Character can collect.");
                    StartCoroutine(Pickup(character));
                }
                else
                {
                    Debug.Log("Character cannot collect right now.");
                }
            }
            else
            {
                Debug.LogWarning("Character component not found on player.");
            }
        }
    }

    IEnumerator Pickup(Character character)
    { 
        // Randomly choose to speed up or slow down
        float speedBuff = speedBuffValue;
        
        // Apply effect to the player
       character.IncreaseSpeed(speedBuff);
       /* if(speedBuff > 0.0f)
        { Instantiate(pickupEffectspeedup, transform.position, transform.rotation); }
        else { Instantiate(pickupEffectslowdown, transform.position, transform.rotation); }*/
        // Disable the collectible's visual representation and collider
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;

        pinkCloudVFX.Play();
        //play pickup sound
        source.PlayOneShot(clip);

        // Set the character to not able to collect more
        character.SetCollectState(false);

        // Wait for the duration of the buff
        yield return new WaitForSeconds(duration);

        // Reverse the effect on the player
       character.DecreaseSpeed(speedBuff);

        // Wait for the cooldown period before allowing the player to collect another collectible
        yield return new WaitForSeconds(pickupCooldown - duration);

        // Set the character to be able to collect again
       character.SetCollectState(true);

        
        // Remove the power-up object
        Destroy(gameObject);
     
    }
}

