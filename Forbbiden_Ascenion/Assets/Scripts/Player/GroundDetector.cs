using UnityEngine;
using System;
public class GroundDetector : MonoBehaviour
{
    public ContactFilter2D contactFilter2D;

    [SerializeField] private Rigidbody2D _rb2d;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    public bool IsGrounded => Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
}
