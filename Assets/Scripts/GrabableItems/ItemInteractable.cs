using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.XR;

public class ItemInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private string idObject;
    [SerializeField] private PlayerInteract player;
    [SerializeField] private UnityEvent isTaked;

    private bool isPickable;
    private Rigidbody rigidBody;
    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    public void Interact()
    {
        isPickable = false;
        Quaternion rotate = new Quaternion(0, 0, 0, 0);
        transform.rotation = rotate;

        player.GrabItem(this.gameObject, idObject);

        rigidBody.useGravity = false;
        rigidBody.isKinematic = true;
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Collider>().enabled = false;
        if (!isPickable)
        {
            isTaked.Invoke();
        }
        transform.rotation = rotate;
    }
}
