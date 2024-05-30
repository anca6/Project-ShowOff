using UnityEngine;

public class Target : MonoBehaviour
{
    //properties for the target gameobject
    [SerializeField] private GameObject targetPlatform;

    private bool isHit = false;

    public bool IsHit
    {
        get { return isHit; }
    }

    public void MarkAsHit()
    {
        isHit = true;
    }
}