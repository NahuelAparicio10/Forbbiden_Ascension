using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;
    [SerializeField] private float _customGravityScale;
    private float _originalGravity;

    private Rigidbody2D _rb2d;
    private Vector2 _lastMoveDir;

    public float _dashDuration;
    private float _currentTime;
    private bool _isDashing;



    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _originalGravity = _rb2d.gravityScale;
    }

    private void Update()
    {
        if(_isDashing)
        {
            _currentTime += Time.deltaTime;
            
            if(_currentTime >= _dashDuration)
            {
                _isDashing = false;
                _currentTime = 0;
                _rb2d.velocity = Vector2.zero;
            }
        }

        if(_rb2d.velocity.y < 0)
        {
            _rb2d.gravityScale = _customGravityScale;
        }
        else
        {
            _rb2d.gravityScale = _originalGravity;
        }
    }


    public void Move(Vector2 moveDir)
    {
        if (_isDashing) return;

        if(moveDir.sqrMagnitude > 0.01f)
        {
            _lastMoveDir = moveDir;
        }

        _rb2d.velocity = new Vector2(moveDir.x * _movementSpeed, _rb2d.velocity.y);
    }

    public void Jump() => _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);

    public void Dash(bool isMoving, Vector2 moveDir)
    {
        if(_isDashing) return;

        _isDashing = true;
        _currentTime = 0;

        if(!isMoving)
        {
            moveDir = _lastMoveDir;
        }
        _rb2d.velocity = moveDir * _dashForce;
    }


    public void ResetVelocity()
    {
        if (_isDashing) return;
        _rb2d.velocity = Vector2.zero;
    }

}
