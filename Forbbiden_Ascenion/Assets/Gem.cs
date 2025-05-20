using UnityEngine;

public class Gem : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameManager.Instance.InvokeCollectGem();
            Destroy(gameObject);
        }
    }
}
