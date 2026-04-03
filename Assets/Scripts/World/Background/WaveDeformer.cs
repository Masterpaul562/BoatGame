using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaveDeformer : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 1f;
    public float frequency = 1.5f;
    public float speed = 1f;
    public float horizontalOffset = 0f;

    [Header("Boat Speed Influence")]
    public FishEngineReal engine;
    public float knotSpeedMultiplier = 0.2f;

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] vertices;
    private float waveTime;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
        vertices = new Vector3[baseVertices.Length];
    }

    void Update()
    {
        float currentSpeed = speed;

        if (engine != null)
        {
            currentSpeed += engine.knots * knotSpeedMultiplier;
        }

        // Smooth continuous phase accumulation
        waveTime += Time.deltaTime * currentSpeed;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = baseVertices[i];
            float worldX = transform.TransformPoint(v).x + horizontalOffset;

            v.z = baseVertices[i].z + Mathf.Sin(worldX * frequency + waveTime) * amplitude;

            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeight(float worldX)
    {
        float wave = Mathf.Sin((worldX + horizontalOffset) * frequency + waveTime) * amplitude;

        Vector3 localOffset = new Vector3(0f, 0f, wave);
        Vector3 worldOffset = transform.TransformDirection(localOffset);

        return worldOffset.y;
    }
}