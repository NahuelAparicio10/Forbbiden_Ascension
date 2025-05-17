using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerMovement _movement;
    private PlayerInputs _inputs;
    private PlayerAnimations _animations;
    [SerializeField] private GroundDetector _groundDetector;
    [SerializeField] private PlayerInteractable _playerInteractable;
    [SerializeField] private PlayerDie _die;

    private DialogueController _dialogueController;

    public bool isTalking;

    public Vector2 currentCheckPoint;

    [SerializeField] private float coyoteTime = 0.15f;
    private float coyoteTimer;

    public GameObject deadCanvas;

    private void Awake()
    {
        _movement = GetComponent<PlayerMovement>();
        _inputs = GetComponent<PlayerInputs>();
        _animations = GetComponent<PlayerAnimations>();
        _dialogueController = FindFirstObjectByType<DialogueController>();
    }

    void Start()
    {
        _die.OnDie += OnDead;
        _inputs.OnDash += DashAction;
        _inputs.OnInteract += InteractAction;
        _inputs.OnJump += JumpAction;
        _dialogueController.DialogueStarted += OnDialogueStarted;
        _dialogueController.DialogueEnded += OnDialogueEnded;
        GameManager.Instance.OnPlayAgain += PlayAgain;
        isTalking = false;
    }

    void Update()
    {
        _animations.SetIsGrounded(_groundDetector.IsGrounded);

        if (_groundDetector.IsGrounded)
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;
    }

    public bool isFalling = false;

    private void FixedUpdate()
    {
        if (isTalking) return;

        if(!_groundDetector.IsGrounded && !isFalling)
        {
            isFalling = true;
           _animations.FallingAnimation();
        }
        if(_groundDetector.IsGrounded)
        {
            isFalling = false;
        }

        if (_inputs.IsMoving())
        {
            _animations.MoveAnimation(true);
            _animations.FlipSprite(_inputs.RawAxisX());
            _movement.Move(_inputs.RawAxisX(), _groundDetector.IsGrounded);
        }
        else
        {
            _animations.MoveAnimation(false);
            if(_groundDetector.IsGrounded && !_animations.IsJumping)
            {
                _movement.ResetVelocity();
            }
        }
    }

    private void PlayAgain()
    {
        transform.position = currentCheckPoint;
    }

    private void OnDead()
    {
        deadCanvas.SetActive(true);
    }

    private void OnDialogueEnded()
    {
        isTalking = false;
    }

    private void OnDialogueStarted()
    {
        isTalking = true;
    }

    private void JumpAction()
    {
        if (isTalking || coyoteTimer <= 0f) return;

        _animations.JumpAnimation();
        GameManager.Instance.AddJump();
        _movement.Jump();
    }

    private void InteractAction()
    {
        if (isTalking) return;

        _playerInteractable.InteractPerformed();
    }

    private void DashAction()
    {
        if (isTalking) return;
        _movement.Dash(_inputs.RawAxisX());
    }

    private void OnDestroy()
    {
        _die.OnDie -= OnDead;
        _inputs.OnDash -= DashAction;
        _inputs.OnInteract -= InteractAction;
        _inputs.OnJump -= JumpAction;
        _dialogueController.DialogueStarted -= OnDialogueStarted;
        _dialogueController.DialogueEnded -= OnDialogueEnded;
        GameManager.Instance.OnPlayAgain -= PlayAgain;

    }
}
