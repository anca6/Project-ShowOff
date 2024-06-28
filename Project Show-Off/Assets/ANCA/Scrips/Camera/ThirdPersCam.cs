using UnityEngine;

public class ThirdPersCam : MonoBehaviour
{
    //properties for third person camera movement
    [SerializeField] private Transform orientation;
    [SerializeField] private Transform player;
    [SerializeField] private Rigidbody rb;

    //moving after the player with a slight offset
    private void Update()
    {
        Vector3 viewDir = player.position - new Vector3(transform.position.x, player.position.y, transform.position.z);
        orientation.forward = viewDir.normalized;

    }
}
