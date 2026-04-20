using UnityEngine;
using UnityEngine.Rendering.Universal;

public class CandleFlicker : MonoBehaviour
{
    public Light2D light2D;

    [Header("Flicker Settings")]
    public float minIntensity = 0.8f;
    public float maxIntensity = 1.2f;
    public float flickerSpeed = 2f;

    [Header("Optional Color Flicker")]
    public Color baseColor = new Color(1f, 0.85f, 0.6f);
    public float colorVariation = 0.05f;

    private float noiseOffset;

    void Start()
    {
        if (light2D == null)
            light2D = GetComponent<Light2D>();

        noiseOffset = Random.Range(0f, 100f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(Time.time * flickerSpeed, noiseOffset);

        // Smooth intensity flicker
        float intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        light2D.intensity = intensity;

        // Subtle color variation (optional)
        float colorNoise = Mathf.PerlinNoise(Time.time * flickerSpeed + 50f, noiseOffset);
        float variation = (colorNoise - 0.5f) * colorVariation;

        light2D.color = new Color(
            baseColor.r,
            baseColor.g + variation,
            baseColor.b
        );
    }
}