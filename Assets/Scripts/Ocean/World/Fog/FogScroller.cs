using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class FogScroller : MonoBehaviour
{
    [Header("Scroll Settings")]
    public float baseScrollSpeed = 0.1f;

    [Header("Boat Engine Reference")]
    public FishEngineReal engine;

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
        float knots = 0f;

        if (engine != null)
        {
            knots = engine.knots;
        }

        float speedMultiplier = 1f + (knots / 10f);

        offset.x += baseScrollSpeed * speedMultiplier * Time.deltaTime;
        rend.material.mainTextureOffset = offset;

        ApplyOpacity();
    }

    void ApplyOpacity()
    {
        Color color = rend.material.color;
        color.a = opacity;
        rend.material.color = color;
    }
}