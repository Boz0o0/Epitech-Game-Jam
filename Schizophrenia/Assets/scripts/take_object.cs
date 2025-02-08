using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TakeObject : MonoBehaviour
{
    public Transform player;
    public Transform playerCam;
    public float pickUpRange = 2f;
    public float throwForce = 10f;

    private GameObject heldObject;
    private bool isHolding = false;

    private void Update()
    {
        if (isHolding)
        {
            heldObject.transform.position = playerCam.position + playerCam.forward * 1.5f;
            heldObject.transform.rotation = playerCam.rotation;
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (isHolding)
            {
                DropObject();
            }
            else
            {
                PickUpObject();
            }
        }
    }

    private void PickUpObject()
    {
        RaycastHit hit;
        if (Physics.Raycast(playerCam.position, playerCam.forward, out hit, pickUpRange))
        {
            if (hit.transform.CompareTag("Pickable"))
            {
                heldObject = hit.transform.gameObject;
                heldObject.GetComponent<Rigidbody>().isKinematic = true;
                heldObject.transform.SetParent(playerCam);
                isHolding = true;
            }
        }
    }

    private void DropObject()
    {
        if (heldObject != null)
        {
            heldObject.GetComponent<Rigidbody>().isKinematic = false;
            heldObject.transform.SetParent(null);
            heldObject.GetComponent<Rigidbody>().AddForce(playerCam.forward * throwForce, ForceMode.Impulse);
            heldObject = null;
            isHolding = false;
        }
    }
}
