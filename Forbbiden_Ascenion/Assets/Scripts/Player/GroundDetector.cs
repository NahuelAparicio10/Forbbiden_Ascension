using UnityEngine;
using System;
public class GroundDetector : MonoBehaviour
{
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask _lm;

    public event Action<bool> OnGroundChanged;
    public bool IsGrounded { get; private set; }


    private bool _wasGrounded;

    void FixedUpdate()
    {
        IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, _lm);
        
        if(_wasGrounded != IsGrounded)
        {
            OnGroundChanged?.Invoke(IsGrounded);
            _wasGrounded = IsGrounded;
        }
    }
}
