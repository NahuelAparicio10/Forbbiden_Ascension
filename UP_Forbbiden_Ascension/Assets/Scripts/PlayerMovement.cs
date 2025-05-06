using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private PlayerController _player;

    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _dashForce;

    private Rigidbody2D _rb2d;
    private Vector2 _lastMoveDir;

    private void Awake()
    {
        _rb2d = GetComponent<Rigidbody2D>();
        _player = GetComponent<PlayerController>();
    }

    void Update()
    {
        
    }

    public void Move(float moveDirX)
    {
        _lastMoveDir = new Vector2(moveDirX, 0).normalized;
        _rb2d.linearVelocity = new Vector2(moveDirX * _movementSpeed, _rb2d.linearVelocity.y);
    }

    public void Jump()
    {
        _rb2d.linearVelocityY = 0;

        _rb2d.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    public void Dash()
    {
        _rb2d.AddForce(_lastMoveDir * _dashForce, ForceMode2D.Impulse);
    }


}
