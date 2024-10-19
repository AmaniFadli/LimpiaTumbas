using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public static PlayerInput instance;
    private Vector2 playerInput;

    private Vector3 playerTransform;
    void Start()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    private void Update()
    {
        playerTransform = transform.position;
    }
    public void OnMove(InputValue move)
    {
        playerInput = move.Get<Vector2>();
    }
    public Vector2 GetPlayerInput()
    {
        return playerInput;
    }

    public Vector3 GetPlayerPosition()
    {
        return playerTransform;
    }
}
