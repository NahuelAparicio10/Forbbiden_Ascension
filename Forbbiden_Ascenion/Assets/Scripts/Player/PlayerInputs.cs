using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerInputsSystem _inputs;

    public event Action OnInteract;
    public event Action OnJump;
    public event Action OnDash;
    public event Action OnMoveCanceled;

    private void Awake()
    {
        _inputs = new PlayerInputsSystem();
        _inputs.Player.Enable();
        _inputs.Player.Interact.performed += Interact_performed;
        _inputs.Player.Jump.performed += Jump_performed;
        _inputs.Player.Dash.performed += Dash_performed;
        _inputs.Player.Move.canceled += Move_canceled;
    }

    private void Move_canceled(InputAction.CallbackContext context)
    {
        OnMoveCanceled?.Invoke();
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    public Vector2 MoveDir() => _inputs.Player.Move.ReadValue<Vector2>().normalized;

    public bool IsMoving() => MoveDir().normalized.x > 0.01f || MoveDir().normalized.x < -0.1f;

    private void Jump_performed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Dash_performed(InputAction.CallbackContext context)
    {
        OnDash?.Invoke();
    }

}
