using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesForReal : MonoBehaviour
{
    [SerializeField] float amplitude1, wavelength1, amplitude2, wavelength2;
    private float phase, phaseX, x, y;
    public bool test;

    private Mesh mesh;

    [SerializeField] Vector3[] vertices;

    private void Start()
    {
       
    }

    void Update()
    {

        mesh = GetComponent<MeshFilter>().mesh;
        vertices = mesh.vertices;
        CalcWave();
       
        

       for (int i = 0; i <11; i++)
        {
            for (int j = 0; j < 44; j++)
            {
                Debug.Log(j);
                vertices[j].z += y;
                vertices[j].x += x;
            }
        }

        mesh.vertices = vertices;




    }


    private void CalcWave()
    {

        phaseX += Time.deltaTime;
        amplitude1 = 3;
        wavelength1 = 8;
        y = 0;
        x = phaseX;

        x -= amplitude1 * Mathf.Sin(phaseX / wavelength1 - Time.deltaTime / Mathf.Sqrt(wavelength1));
        y += amplitude1 * Mathf.Cos(phaseX / wavelength1 - Time.deltaTime / Mathf.Sqrt(wavelength1));


        amplitude2 = .5f;
        wavelength2 = 2;
        x -= amplitude2 * Mathf.Sin(phaseX / wavelength2 - Time.deltaTime / Mathf.Sqrt(wavelength2));
        y += amplitude2 * Mathf.Cos(phaseX / wavelength2 - Time.deltaTime / Mathf.Sqrt(wavelength2));
       // transform.position = new Vector3(x, y, transform.position.z);

    }

}
//float y = Mathf.Sin( time- Time.deltaTime); normal sin wave for refrence
