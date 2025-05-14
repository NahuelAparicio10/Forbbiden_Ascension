using UnityEngine;

public class SimpleParallax : MonoBehaviour
{

    private float _startPosition;
    public GameObject cameraGo;
    public float parallaxSpeed;

    void Start()
    {
        _startPosition = cameraGo.transform.position.x;
    }

    void LateUpdate() // Usamos LateUpdate para evitar jitter por diferencia con movimiento de la cámara
    {
        float dist = (cameraGo.transform.position.x - _startPosition) * parallaxSpeed;
        transform.position = new Vector3(_startPosition + dist, transform.position.y, transform.position.z);
    }
}
