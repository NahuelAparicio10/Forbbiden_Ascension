using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;


    private Rigidbody2D _rb2d;
    private Vector2 _lastMoveDir;

    private float _dashDuration;
    private float _currentTime;
    private bool _isDashing;
    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
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
            }
        }
    }


    public void Move(Vector2 moveDir)
    {
        if(moveDir.sqrMagnitude > 0.01f)
        {
            _lastMoveDir = moveDir;
        }

        _rb2d.velocity = new Vector2(moveDir.x * _movementSpeed, _rb2d.velocity.y);
        //_rb2d.linearVelocity = new Vector2(moveDir.x * _movementSpeed, _rb2d.linearVelocity.y);
    }

    public void Jump() => _rb2d.velocity = new Vector2(_rb2d.velocity.x, _jumpForce);

    public void Dash(Vector2 moveDir)
    {
        _isDashing = true;
        _rb2d.velocity = moveDir * _dashForce;
    }


    public void ResetVelocity()
    {
        _rb2d.velocity = Vector2.zero;
    } 



}
