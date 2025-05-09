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
        _inputs.OnMoveCanceled += MoveCanceled;
        _dialogueController.DialogueStarted += OnDialogueStarted;
        _dialogueController.DialogueEnded += OnDialogueEnded;
        isTalking = false;
    }

    private void MoveCanceled()
    {
        if(_groundDetector.IsGrounded)
            _movement.ResetVelocity();
    }

    private void OnDialogueEnded()
    {
        isTalking = false;
    }

    private void OnDialogueStarted()
    {
        _movement.ResetVelocity();
        isTalking = true;
    }

    private void JumpAction()
    {
        if (isTalking || !_groundDetector.IsGrounded) return;

        _animations.JumpAnimation();

        _movement.Jump();
    }

    private void InteractAction()
    {
        if(isTalking) return;
        Debug.Log("Interacted");
    }

    private void DashAction()
    {
        if (isTalking) return;
        _movement.Dash(_inputs.IsMoving(), _inputs.MoveDir());
    }

    void Update()
    {
        _animations.SetIsGrounded(_groundDetector.IsGrounded);
    }

    private void FixedUpdate()
    {
        if (isTalking) return;

        if (_inputs.IsMoving())
        {
            _animations.MoveAnimation(true);
            _animations.FlipSprite(_inputs.MoveDir().x);
            _movement.Move(_inputs.MoveDir());
        }
        else
        {
            _animations.MoveAnimation(false);
        }
    }

    private void OnDestroy()
    {
        _dialogueController.DialogueStarted -= OnDialogueStarted;
        _dialogueController.DialogueEnded -= OnDialogueEnded;
    }
}
