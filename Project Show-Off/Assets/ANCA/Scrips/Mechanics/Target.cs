using UnityEngine;

public class Target : MonoBehaviour
{
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