using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody playerRigid;
    public float moveSpeed;
    public float jumpForce;
    private bool isJumping = false;

    public Transform playerCamera;
    public float mouseSensitivity = 100f;
    private float verticalRotation = 0f;

    void Update()
    {
        // Check for jump input
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        // Get horizontal and vertical input values
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement vector based on camera direction
        Vector3 cameraForward = playerCamera.forward;
        cameraForward.y = 0f;
        cameraForward.Normalize();
        Vector3 movement = (cameraForward * verticalInput + playerCamera.right * horizontalInput) * moveSpeed;
        playerRigid.velocity = new Vector3(movement.x, playerRigid.velocity.y, movement.z);

        // Rotate the player based on mouse input
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.fixedDeltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.fixedDeltaTime;

        // Rotate the player horizontally
        playerRigid.rotation *= Quaternion.Euler(0f, mouseX, 0f);

        // Rotate the player camera vertically
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }

    void Jump()
    {
        // Apply jump force
        playerRigid.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isJumping = true;
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player has landed on the ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }
}
