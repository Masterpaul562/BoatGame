using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMesh : MonoBehaviour
{
    public Material mat;
    private Mesh mesh;
    [SerializeField] Waves wave;
    [SerializeField] Vector3[] vertices;
    private bool once;




    private void Start()
    {
        for (int i = 0; i < 11; i++)
        {
            wave.xValueIncrease[i] = Random.Range(0.05f, 0.1f);
        }
    }

    void Update()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;



        for (int i = 0; i < 11; i++)
        {            
            vertices[i].z = wave.WavePos(2,i)* 3;
            if (vertices[i].z < 0)
            {
                vertices[i].z *= -1;
            }
        }
        mesh.vertices = vertices;
       
        
       // GetComponent<MeshRenderer>().material = mat;
    }
   


}
