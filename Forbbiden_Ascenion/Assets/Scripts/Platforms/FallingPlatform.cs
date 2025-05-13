using UnityEngine;

public class FallingPlatform : MonoBehaviour
{
    public Transform restingPosition;      
    public Transform loweredPosition;      
    public float speed = 2f;

    private bool playerOnPlatform = false;
    private Vector3 targetPosition;

    void Start()
    {
        targetPosition = restingPosition.position;
    }

    void Update()
    {
        targetPosition = playerOnPlatform ? loweredPosition.position : restingPosition.position;

        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddCloudTouched();
            playerOnPlatform = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerOnPlatform = false;
    }
}

