using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform cameraTransform;
    private MovementBehaviour MVB;
    void Start()
    {
        MVB = GetComponent<MovementBehaviour>();
        cameraTransform = Camera.main.transform;
    }

    void Update()
    {
        Movement();
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
}
