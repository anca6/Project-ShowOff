using UnityEngine;

public class TargetPlatform : MonoBehaviour
{
    [SerializeField] private float speed = 2.0f;
    [SerializeField] private float moveDistance;
    [SerializeField] private bool moveHorizontally = false;

    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 targetPos;
    private bool isMoving = false;

    private void Start()
    {
        startPos = transform.position;
        if (moveHorizontally)
        {
            endPos = new Vector3(startPos.x + moveDistance, startPos.y, startPos.z);
        }
        else
        {
            endPos = new Vector3(startPos.x, startPos.y + moveDistance, startPos.z);
        }
        targetPos = endPos;
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            MovePlatform();
        }
    }

    private void MovePlatform()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
        if (transform.position == targetPos)
        {
            targetPos = targetPos == endPos ? startPos : endPos;
        }
    }

    public void StartMoving()
    {
        isMoving = true;
    }

    public void StopMoving()
    {
        isMoving = false;
    }

    //uncheck when we add the platform model
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
