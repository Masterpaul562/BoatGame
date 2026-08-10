using UnityEngine;

public class FadeBlackBox : MonoBehaviour
{
    public SpriteRenderer blackBox;
    public float fadeSpeed = 2f;

    private float targetAlpha = 1f;

    void Update()
    {
        Color color = blackBox.color;

        color.a = Mathf.MoveTowards(
            color.a,
            targetAlpha,
            fadeSpeed * Time.deltaTime
        );

        blackBox.color = color;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetAlpha = 0f;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            targetAlpha = 1f;
        }
    }
}