using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish_VFX : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject finishParticle;
    void Start()
    {
        if (finishParticle == null)
        {
            Debug.LogWarning("No finishParticle has been assighned on: " + gameObject);
            return;
        }

        if (finishParticle == true)
        {
            finishParticle.SetActive(false);
            Debug.Log("sett particles to false");
        }
    }

    private void OnTriggerEnter(Collider Other)
    {
        Debug.Log("collison detected");

        if (Other.CompareTag("Player"))
        { 
        finishParticle.SetActive(true);
        Debug.Log("sett particles to true");
        }
    }
}
