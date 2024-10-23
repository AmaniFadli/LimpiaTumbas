using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    private Vector2 playerInput;
    private bool jumpInput;
    private bool interactInput;
    private int shootInput;
    private bool tabInput;
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

    public void OnShoot(InputValue value)
    {
        if (value.isPressed)
        {
            shootInput = 1;
        }
        else
        {
            shootInput = 0;
        }
    }
    public int GetShootInput()
    {
        return shootInput;
    }

    public void OnNote(InputAction.CallbackContext ctx)
    {
        if(!tabInput)
        {
            tabInput = true;
        }
        else
        {
            tabInput = false;
        }
    }
    public bool GetTabInput()
    {
        return tabInput;
    }
}
