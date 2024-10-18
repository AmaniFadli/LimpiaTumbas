using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PCamera : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity;
    [SerializeField] InputAction look;

    float rotationX;
    [SerializeField] float minRotationX, maxRotationX;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        Vector2 deltaValue = look.ReadValue<Vector2>();

        Quaternion lookRotation = Quaternion.AngleAxis(deltaValue.x * Time.deltaTime * mouseSensitivity, Vector3.up);

        transform.localRotation = lookRotation * transform.localRotation;

        rotationX -= deltaValue.y * Time.deltaTime * mouseSensitivity;
        rotationX = Mathf.Clamp(rotationX, minRotationX, maxRotationX);

        cameraTransform.localRotation = Quaternion.Euler(rotationX, 0, 0);

    }

    private void OnEnable()
    {
        look.Enable();
    }
    private void OnDisable()
    {
        look.Disable();
        Cursor.lockState = CursorLockMode.Confined;
    }
}
