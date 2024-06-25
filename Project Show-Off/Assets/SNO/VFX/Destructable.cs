using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructable : MonoBehaviour
{
    
    public GameObject destroyedVersion;

    public void DestroyWall()
    {
        Instantiate(destroyedVersion, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }
}