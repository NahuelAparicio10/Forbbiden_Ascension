using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
    [SerializeField] private float speed = 2f;
    private Vector3 _target;

    private void Start()
    {
        _target = pointB.position;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, _target) < 0.1f)
        {
            _target = _target == pointA.position ? pointB.position : pointA.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.AddCloudTouched();
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.transform.SetParent(null);
        }
    }
}
