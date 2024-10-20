using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform cameraTransform;
    private MovementBehaviour MVB;
    private Rigidbody rb;

    [SerializeField] private float jumpForce = 5f;

    private bool isGrounded = true;

    void Start()
    {
        MVB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Movement();
        Jump();
    }
    private void Movement()
    {
        Vector2 playerWasp = PlayerInput.instance.GetPlayerInput();

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = 0;
        cameraRight.y = 0;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 input = cameraForward * playerWasp.y + cameraRight * playerWasp.x;
        MVB.MoveIsometric(input);
    }

    private void Jump()
    {
        if (PlayerInput.instance.GetJumpInput() && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
