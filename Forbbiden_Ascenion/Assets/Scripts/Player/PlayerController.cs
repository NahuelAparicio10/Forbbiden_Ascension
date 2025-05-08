using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInputs _inputs;
    private PlayerAnimations _animations;
    [SerializeField] private GroundDetector _groundDetector;

    private DialogueController _dialogueController;

    public bool isTalking;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _inputs = GetComponent<PlayerInputs>();
        _animations = GetComponent<PlayerAnimations>();
        _dialogueController = FindFirstObjectByType<DialogueController>();
    }

    void Start()
    {
        _inputs.OnDash += DashAction;
        _inputs.OnInteract += InteractAction;
        _inputs.OnJump += JumpAction;
        _groundDetector.OnGroundChanged += GroundChanged;
        _dialogueController.DialogueStarted += OnDialogueStarted;
        _dialogueController.DialogueEnded += OnDialogueEnded;
        isTalking = false;
    }

    private void OnDialogueEnded() => isTalking = false;
    private void OnDialogueStarted() => isTalking = true;

    private void GroundChanged(bool isGrounded)
    {
    }

    private void JumpAction()
    {
        if (isTalking) return;

        //_animations.JumpAnimation();
        _movement.Jump();
    }

    private void InteractAction()
    {
        if(isTalking) return;

        throw new NotImplementedException();
    }

    private void DashAction()
    {
        if (isTalking) return;

        _movement.Dash(_inputs.MoveDir());
    }

    void Update()
    {
        if (isTalking) return;

        if (_inputs.MoveDir().sqrMagnitude > 0.01f)
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
        _groundDetector.OnGroundChanged -= GroundChanged;
        _dialogueController.DialogueStarted -= OnDialogueStarted;
        _dialogueController.DialogueEnded -= OnDialogueEnded;
    }
}
