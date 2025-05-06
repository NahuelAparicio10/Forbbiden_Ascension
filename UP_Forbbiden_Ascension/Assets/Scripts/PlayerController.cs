using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInputs _inputs;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _inputs = GetComponent<PlayerInputs>();
    }

    void Start()
    {
        _inputs.OnDash += DashAction;
        _inputs.OnInteract += InteractAction;
        _inputs.OnJump += JumpAction;
    }

    private void JumpAction()
    {
        _movement.Jump();
    }

    private void InteractAction()
    {
        throw new NotImplementedException();
    }

    private void DashAction()
    {
        _movement.Dash();
    }

    void Update()
    {
        if(_inputs.MoveDirX() > 0.1f)
        {
            //Is Moving For animation purpose
            _movement.Move(_inputs.MoveDirX());
        }
        else
        {
            //Is Not Moving For animation purpose
        }
    }

    private void OnDestroy()
    {
        
    }
}
