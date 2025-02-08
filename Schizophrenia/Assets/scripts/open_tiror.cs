using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OpenTiror : MonoBehaviour
{
    public Transform tirorTransform;
    public Vector3 openPosition;
    public Vector3 closedPosition;
    public float openSpeed = 2f;

    private bool isOpen = false;
    private bool isAnimating = false;

    private void Update()
    {
        if (isAnimating)
        {
            Vector3 targetPosition = isOpen ? openPosition : closedPosition;
            tirorTransform.localPosition = Vector3.Lerp(tirorTransform.localPosition, targetPosition, Time.deltaTime * openSpeed);

            if (Vector3.Distance(tirorTransform.localPosition, targetPosition) < 0.01f)
            {
                tirorTransform.localPosition = targetPosition;
                isAnimating = false;
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && !isAnimating)
        {
            isOpen = !isOpen;
            isAnimating = true;
        }
    }
}
