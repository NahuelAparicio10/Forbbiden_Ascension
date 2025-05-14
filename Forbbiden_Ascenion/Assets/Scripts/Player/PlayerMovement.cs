using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _airSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _customGravityScale;
    private float _originalGravity;

    private Rigidbody2D _rb2d;
    private Vector2 _lastMoveDir;

    [Header("Dash Variables")]
    [SerializeField] private bool canDash = true;
    [SerializeField] private bool _isDashing;
    [SerializeField] private float dashingPower = 24f;
    [SerializeField] private float dashingTime = 0.2f;
    [SerializeField] private float dashingCooldown = 1f;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _originalGravity = _rb2d.gravityScale;
    }

    private void FixedUpdate()
    {      
        if(_isDashing)return;

        if(_rb2d.velocity.y < 0)
        {
            _rb2d.gravityScale = _customGravityScale;
        }
        else
        {
            _rb2d.gravityScale = _originalGravity;
        }
    }


    public void Move(float horizontal, bool isGrounded)
    {
        if (_isDashing) return;
        if(isGrounded)
        {
            _rb2d.velocity = new Vector2(horizontal * _movementSpeed, _rb2d.velocity.y);
        }
        else
        {
            _rb2d.velocity = new Vector2(horizontal * _airSpeed, _rb2d.velocity.y);

        }

        _lastMoveDir.x = horizontal;
    }

    public void Jump() => _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);

    public void Dash(float horizontal)
    {
        if (!canDash) return;
        GameManager.Instance.AddDash();
        StartCoroutine(DashCor(horizontal));
    }
    public void ResetVelocity()
    {
        if (_isDashing) return;
        _rb2d.velocity = Vector2.zero;
    }
    private IEnumerator DashCor(float horizontal)
    {
        canDash = false;
        _isDashing = true;
        float originalGravity = _rb2d.gravityScale;
        _rb2d.gravityScale = 0f;
        if(horizontal == 0)
        {
            _rb2d.velocity = new Vector2(_lastMoveDir.x * dashingPower, 0f);
        }
        else
        {
            _rb2d.velocity = new Vector2(horizontal * dashingPower, 0f);
        }
        yield return new WaitForSeconds(dashingTime);
        _rb2d.gravityScale = originalGravity;
        _isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

}
