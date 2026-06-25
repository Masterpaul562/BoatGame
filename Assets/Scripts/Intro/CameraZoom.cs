using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraIntroZoom : MonoBehaviour
{
    [Header("Position")]
    public Vector3 startPosition;
    public Vector3 endPosition;

    [Header("Zoom")]
    public float startSize = 5f;
    public float endSize = 8f;

    [Header("Timing")]
    public float duration = 10f;

    private Camera cam;
    private float timer;

    void Start()
    {
        cam = GetComponent<Camera>();

        transform.position = startPosition;
        cam.orthographicSize = startSize;
    }

    void Update()
    {
        if (timer >= duration)
            return;

        timer += Time.deltaTime;

        float t = Mathf.Clamp01(timer / duration);

        // Smooth easing
        t = Mathf.SmoothStep(0f, 1f, t);

        transform.position = Vector3.Lerp(startPosition, endPosition, t);
        cam.orthographicSize = Mathf.Lerp(startSize, endSize, t);
    }
}