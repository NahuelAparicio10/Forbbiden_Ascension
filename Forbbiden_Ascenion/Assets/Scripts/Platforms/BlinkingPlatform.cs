using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    public float blinkDuration = 2f;       
    public float disappearDuration = 2f;   

    public float blinkInterval = 0.2f;     

    [SerializeField]private SpriteRenderer sr;
    private Collider2D col;
    private bool isTriggered = false;

    void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isTriggered && collision.collider.CompareTag("Player"))
        {
            isTriggered = true;
            StartCoroutine(BlinkAndDisappear());
        }
    }

    IEnumerator BlinkAndDisappear()
    {
        float timer = 0f;
        while (timer < blinkDuration)
        {
            sr.enabled = !sr.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        sr.enabled = false;
        col.enabled = false;

        yield return new WaitForSeconds(disappearDuration);

        sr.enabled = true;
        col.enabled = true;
        isTriggered = false; 
    }
}
