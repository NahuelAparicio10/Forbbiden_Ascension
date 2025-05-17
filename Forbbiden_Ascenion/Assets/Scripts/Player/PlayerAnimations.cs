using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private Animator animator;


    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
    }
    public bool IsJumping => animator.GetBool("IsJumping");
    public void SetIsGrounded(bool isGrounded) => animator.SetBool("IsGrounded", isGrounded);

    public void FallingAnimation()
    {
        if (IsJumping) return;

        animator.CrossFade("Falling", 0.2f);
    }

    public void JumpAnimation()
    {
        animator.CrossFade("StartJump", 0.2f);
    }

    public void Dash()
    {
        animator.CrossFade("Dash", 0.2f);
    }

    public void MoveAnimation(bool isMoving)
    {
        if(animator.GetBool("IsMoving") != isMoving)
        {
            animator.SetBool("IsMoving", isMoving);
        }
    }

    public void FlipSprite(float x)
    {
        if(x >= 0)
        {
            _spriteRenderer.flipX = false;
        }
        else
        {
            _spriteRenderer.flipX = true;
        }
    }
}
