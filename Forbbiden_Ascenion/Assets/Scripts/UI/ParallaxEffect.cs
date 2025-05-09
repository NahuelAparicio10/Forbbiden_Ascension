using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    private float _length;
    private float _startPosition;
    public GameObject cameraGo;
    public float parallaxSpeed;
    void Start()
    {
        _startPosition = transform.position.x;
        _length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    void FixedUpdate()
    {
        float dist = cameraGo.transform.position.x * parallaxSpeed;
        float movement = cameraGo.transform.position.x * (1 * parallaxSpeed);

        transform.position = new Vector3(_startPosition + dist, transform.position.y, transform.position.z);
        if (movement > _startPosition + _length)
        {
            _startPosition += _length;
        }
        else if (movement < _startPosition - _length)
        {
            _startPosition -= _length;
        }
    }
}
