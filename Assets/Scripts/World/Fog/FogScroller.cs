using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FogScroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float scrollSpeed = 0.1f;

    [Header("Opacity Settings")]
    [Range(0f, 1f)]
    public float opacity = 0.5f;

    private Renderer rend;
    private Vector2 offset;

    void Start()
    {
        rend = GetComponent<Renderer>();
        ApplyOpacity();
    }

    void Update()
    {
        offset.x += scrollSpeed * Time.deltaTime;
        rend.material.mainTextureOffset = offset;

        // Update opacity live in case you change it in Inspector
        ApplyOpacity();
    }

    void ApplyOpacity()
    {
        Color color = rend.material.color;
        color.a = opacity;
        rend.material.color = color;
    }
}