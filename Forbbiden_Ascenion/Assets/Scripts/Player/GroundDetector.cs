using UnityEngine;
using System;
public class GroundDetector : MonoBehaviour
{
    public ContactFilter2D contactFilter2D;

    [SerializeField] private Rigidbody2D _rb2d;

    public bool IsGrounded => _rb2d.IsTouching(contactFilter2D);
}
