using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshTesting : MonoBehaviour
{
    [SerializeField] Vector3[] vertices;
    float phase;
    [SerializeField] private float amp1, amp2, len1, len2;
    private Mesh mesh;

    private void Start()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
           // vertices[i].position = new Vector3(i, vertices[i].position.y, vertices[i].position.z);
        }
    }

    private void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        phase += Time.deltaTime;

        float x = phase;



        for (int i = 0; i < vertices.Length-11; i++)
        {
            float y = 0;
           // amp1 = .5f;
            //len1 = 4;
            x -= amp1 * Mathf.Sin(phase / len1 - (phase - i) / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase / len1 - (phase - i) / Mathf.Sqrt(len1));

           // amp2 = .25f;
           // len2 = 2;
            x -= amp2 * Mathf.Sin(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            y += amp2 * Mathf.Cos(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            vertices[i] = new Vector3(vertices[i].x, vertices[i].y, y );

        }
        mesh.vertices = vertices;

    }
}
