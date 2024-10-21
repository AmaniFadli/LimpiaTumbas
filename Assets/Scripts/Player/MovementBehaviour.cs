using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpForce = 5f;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Move(Vector3 input)
    {
        input.y = 0;
        Vector3 velocityXZ = input.normalized * speed;
        rb.velocity = new Vector3(velocityXZ.x, rb.velocity.y, velocityXZ.z);
    }
    public void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    public void MoveBullet(Vector3 d)
    {
        d.Normalize();
        transform.position += d * speed * Time.deltaTime;
    }
}
