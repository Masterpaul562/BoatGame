using TMPro;
using UnityEngine;
using System.Collections;

public class NotificationUI : MonoBehaviour
{
    [SerializeField] private TMP_Text notificationText;
    [SerializeField] private float fadeTime = 0.3f;
    [SerializeField] private float displayTime = 1.5f;

    private Coroutine currentRoutine;

    public void ShowMessage(string message)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(message));
    }

    private IEnumerator ShowRoutine(string message)
    {
        notificationText.text = message;

        Color color = notificationText.color;
        color.a = 0;
        notificationText.color = color;

        float t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(0, 1, t / fadeTime);
            notificationText.color = color;
            yield return null;
        }

        yield return new WaitForSeconds(displayTime);

        // Fade Out
        t = 0;
        while (t < fadeTime)
        {
            t += Time.deltaTime;
            color.a = Mathf.Lerp(1, 0, t / fadeTime);
            notificationText.color = color;
            yield return null;
        }

        color.a = 0;
        notificationText.color = color;
    }
}