using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class WaveDeformer : MonoBehaviour
{
    [Header("Wave Settings")]
    public float amplitude = 1f;        // vertical wave height
    public float frequency = 1.5f;      // number of crests along the plane
    public float speed = 1f;            // animation speed
    public float horizontalOffset = 0f; // offset along X for back wave

    private Mesh mesh;
    private Vector3[] baseVertices;
    private Vector3[] vertices;

    void Start()
    {

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

            float worldX = transform.TransformPoint(v).x + horizontalOffset;


            v.z += Mathf.Sin(worldX * frequency + time) * amplitude;

            vertices[i] = v;
        }

        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }

    public float GetWaveHeight(float worldX)
    {
        float time = Time.time * speed;

        float wave = Mathf.Sin((worldX + horizontalOffset) * frequency + time) * amplitude;

        Vector3 localOffset = new Vector3(0f, 0f, wave);
        Vector3 worldOffset = transform.TransformDirection(localOffset);

        return worldOffset.y;
    }
}
