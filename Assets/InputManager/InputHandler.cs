using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public Vector2 MoveInput { get; private set; }
    public bool IsMove { get; private set; }

    public Action OnPickupOrDropAction;
    public Action OnInteractAction;

    public void OnMove(InputAction.CallbackContext context)
    {
        MoveInput = context.ReadValue<Vector2>();
        IsMove = MoveInput != Vector2.zero;
    }

    public void OnPickupOrDrop(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            OnPickupOrDropAction?.Invoke();
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.action.WasPerformedThisFrame())
        {
            OnInteractAction?.Invoke();
        }
    }
}
