using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFollowLine : MonoBehaviour
{
    private Vector3[] vertices;
    [SerializeField] private LineRenderer line;
    private Mesh mesh;

    private void Start()
    {
        mesh = mesh = GetComponent<MeshFilter>().mesh;
        //vertices = new Vector3[11];
    }
    private void Update()
    {
        for (int i = 0; i < 11; i++) {
            mesh.vertices[i] = line.GetPosition(i + 11);
        }
    }
}
