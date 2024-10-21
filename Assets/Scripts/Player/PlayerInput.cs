using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    private Vector2 playerInput;
    private bool jumpInput;
    private bool interactInput;
    private float shootInput;

    public UnityEvent onShootEvent;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    public void OnMove(InputValue move)
    {
        playerInput = move.Get<Vector2>();
    }
    public Vector2 GetPlayerInput()
    {
        return playerInput;
    }
    public void OnJump(InputValue jump)
    {
        jumpInput = jump.isPressed;
    }

    public bool GetJumpInput()
    {
        bool jump = jumpInput;
        jumpInput = false; 
        return jump;
    }

    public void OnInteract(InputValue interact)
    {
        interactInput = interact.isPressed;
    }
    public bool GetInteractInput()
    {
        bool interact = interactInput;
        interactInput = false;
        return interact;
    }

    public void OnShoot(InputValue inputValue)
    {
        shootInput = inputValue.Get<float>();

        if (shootInput >= 1)
        {
            onShootEvent?.Invoke();
        }
    }
    public float GetShootInput()
    {
        return shootInput;
    }
}
