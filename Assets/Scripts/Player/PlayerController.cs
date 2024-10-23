using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;    
    private Transform cameraTransform;
    private MovementBehaviour MVB;

    private bool isGrounded = true;
    private Vector3 playerTransform;
    private FMOD.Studio.EventInstance foosteps;
    private Coroutine isMoving;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        MVB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
    }
    public Vector3 GetPlayerPosition()
    {
        return playerTransform;
    }

    void Update()
    {
        playerTransform = transform.position;
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

        if (isMoving == null && playerWasp != Vector2.zero)
        {
            isMoving = StartCoroutine(_PlayFootstep());
        }
        Vector3 input = cameraForward * playerWasp.y + cameraRight * playerWasp.x;
        MVB.Move(input);
    }

    private void Jump()
    {
        if (PlayerInput.instance.GetJumpInput() && isGrounded)
        {
            MVB.Jump();
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

    private IEnumerator _PlayFootstep()
    {
        foosteps = FMODUnity.RuntimeManager.CreateInstance("event:/Footstep");
        foosteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(gameObject));
        foosteps.start();
        foosteps.release();
        yield return new WaitForSeconds(0.4f);
        isMoving = null;
    }
}
