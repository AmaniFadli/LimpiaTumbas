using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    [SerializeField] private float raycastDistance = 10f;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GameObject interactFeedback;
    private GameObject currentFeedback;
    [SerializeField] private Transform interactionZoneLeft;
    [SerializeField] private Transform interactionZoneRight;
    private GameObject grabbeableObj;
    void Start()
    {
        grabbeableObj = null;
    }
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
        if (Physics.Raycast(ray, raycastDistance, layerMask))
        {
            if (currentFeedback == null)
            {
                currentFeedback = Instantiate(interactFeedback);
                TryToInteract();
            }
            else
            {
                currentFeedback.SetActive(true);
                TryToInteract();
            }
        }
        else
        {
            if (currentFeedback != null)
            {
                currentFeedback.SetActive(false);
            }
        }
    }
    public void GrabItem(GameObject grabbeable, string idObject)
    {
        Quaternion rotation = grabbeable.transform.rotation;
        if (idObject.Equals("Linterna"))
        {
            grabbeable.transform.SetParent(interactionZoneLeft, true);
            grabbeable.transform.position = interactionZoneLeft.position;
        }
        else
        {
            grabbeable.transform.SetParent(interactionZoneRight, true);
            grabbeable.transform.position = interactionZoneRight.position;
        }
      
        grabbeableObj = grabbeable;
    }
    public void TryToInteract()
    {
        bool input = PlayerInput.instance.GetInteractInput();
        if(input)
        {
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2f, Screen.height / 2f, 0f));
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, raycastDistance, layerMask))
            {
                IInteractable interactableObject = hitInfo.collider.gameObject.GetComponent<IInteractable>();

                if (interactableObject != null)
                {
                    interactableObject.Interact();
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(gameObject.transform.position, gameObject.transform.forward * raycastDistance);
    }
}
