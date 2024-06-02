using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    //properties for the target gameobject
    [SerializeField] private TargetPlatform targetPlatform;

    [SerializeField] private bool isHit = false;

    public bool IsHit
    {
        get { return isHit; }
    }

    public void MarkAsHit()
    {
        isHit = true;
        targetPlatform.StartMoving();
    }
}