using System;
using UnityEngine;

public class PlayerDie : MonoBehaviour
{
    public event Action OnDie;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Die");
        if(collision.CompareTag("Die"))
        { 
            OnDie?.Invoke();
        }
    }
}
