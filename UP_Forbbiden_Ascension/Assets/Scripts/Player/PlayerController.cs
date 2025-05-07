using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInputs _inputs;
    private PlayerAnimations _animations;
    [SerializeField] private GroundDetector _groundDetector;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _inputs = GetComponent<PlayerInputs>();
        _animations = GetComponent<PlayerAnimations>();
    }

    void Start()
    {
        _inputs.OnDash += DashAction;
        _inputs.OnInteract += InteractAction;
        _inputs.OnJump += JumpAction;
        _groundDetector.OnGroundChanged += GroundChanged;
    }

    private void GroundChanged(bool isGrounded)
    {
    }

    private void JumpAction()
    {
        //_animations.JumpAnimation();
        _movement.Jump();
    }

    private void InteractAction()
    {
        throw new NotImplementedException();
    }

    private void DashAction()
    {
        _movement.Dash(_inputs.MoveDir());
    }

    void Update()
    {
        if(_inputs.MoveDir().sqrMagnitude > 0.01f)
        {
            float dirX = _inputs.MoveDir().x;

            _animations.MoveAnimation(true);
            _animations.FlipSprite(dirX);

            _movement.Move(_inputs.MoveDir());
        }
        else
        {
            _animations.MoveAnimation(false);

            if (!_groundDetector.IsGrounded) return;
            
            _movement.ResetVelocity();
        }

        
    }

    private void OnDestroy()
    {
        
    }
}
