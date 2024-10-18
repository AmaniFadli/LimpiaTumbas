using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void MoveIsometric(Vector3 input)
    {
        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed;
        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z);
    }
}
