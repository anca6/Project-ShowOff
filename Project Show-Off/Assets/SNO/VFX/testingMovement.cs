using UnityEngine;

public class testingMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Speed of movement
    private Rigidbody rb; // Reference to the Rigidbody component

    private void Start()
    {
        // Get the Rigidbody component attached to the player
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Get input from WASD or arrow keys
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Create a Vector3 movement direction based on input
        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Move the player
        MovePlayer(movement);
    }

    private void MovePlayer(Vector3 direction)
    {
        // Calculate the new position
        Vector3 newPosition = rb.position + direction * moveSpeed * Time.deltaTime;

        // Move the Rigidbody to the new position
        rb.MovePosition(newPosition);
    }
}
