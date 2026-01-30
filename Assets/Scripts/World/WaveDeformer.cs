using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaveDeformer : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 1f;       // vertical wave height
    public float frequency = 1.5f;     // number of crests along the plane
    public float speed = 1f;           // animation speed
    public float horizontalOffset = 0f; // offset along X for back wave

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] vertices;

    void Start()
    {
        // Get mesh from MeshFilter
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        float time = Time.time * speed;

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 v = baseVertices[i];

            // Calculate horizontal X position in world space, add optional offset for back wave
            float worldX = transform.TransformPoint(v).x + horizontalOffset;

            // Move vertex along Z (because plane is rotated -90) to make vertical wave
            v.z += Mathf.Sin(worldX * frequency + time) * amplitude;

            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
