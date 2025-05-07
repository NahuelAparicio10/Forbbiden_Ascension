using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private PlayerInputsSystem _inputs;

    public event Action OnInteract;
    public event Action OnJump;
    public event Action OnDash;

    private void Awake()
    {
        _inputs = new PlayerInputsSystem();
        _inputs.Player.Enable();
        _inputs.Player.Interact.performed += Interact_performed;
        _inputs.Player.Jump.performed += Jump_performed;
        _inputs.Player.Dash.performed += Dash_performed;
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
        OnInteract?.Invoke();
    }

    public Vector2 MoveDir() => _inputs.Player.Move.ReadValue<Vector2>().normalized;

    private void Jump_performed(InputAction.CallbackContext context)
    {
        OnJump?.Invoke();
    }

    private void Dash_performed(InputAction.CallbackContext context)
    {
        OnDash?.Invoke();
    }

}
