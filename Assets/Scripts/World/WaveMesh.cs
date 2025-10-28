using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMesh : MonoBehaviour
{
    public Material mat;
    private Mesh mesh;
    [SerializeField] Waves wave;


    void Update()
    {
        mesh = new Mesh();
        Vector3[] vertices = new Vector3[5];

        
            vertices[0] = new Vector3(-10,wave.WavePos(0),0);
            vertices[1] = new Vector3(-10, -10,0);
        vertices[2] = new Vector3(10, -10,0);
        vertices[3] = new Vector3(10, wave.transform.position.y,0);
        vertices[3] = new Vector3(0, wave.transform.position.y+2, 0);


        mesh.vertices = vertices;
        mesh.triangles = new int[] { 0, 1, 2,0,2,3 };
        GetComponent<MeshFilter>().mesh = mesh;
        GetComponent<MeshRenderer>().material = mat;
    }
}
