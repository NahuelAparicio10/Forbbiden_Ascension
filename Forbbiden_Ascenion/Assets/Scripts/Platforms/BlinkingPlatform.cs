using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    public float blinkDuration = 2f;       
    public float disappearDuration = 2f;   

    public float blinkInterval = 0.2f;
    public GameObject go;
    [SerializeField]private SpriteRenderer sr;
    private bool isTriggered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isTriggered && collision.CompareTag("Player"))
        {
            isTriggered = true;
            GameManager.Instance.AddCloudTouched();
            StartCoroutine(BlinkAndDisappear());
        }
    }

    IEnumerator BlinkAndDisappear()
    {
        float timer = 0f;
        bool isWhite = true;

        while (timer < blinkDuration)
        {
            sr.color = isWhite ? Color.black : Color.white;
            isWhite = !isWhite;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }

        sr.enabled = false;
        go.SetActive(false);
        yield return new WaitForSeconds(disappearDuration);

        sr.enabled = true;
        sr.color = Color.white;
        go.SetActive(true);
        isTriggered = false;
    }
}
