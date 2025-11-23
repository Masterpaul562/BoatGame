using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertIceTest : MonoBehaviour
{
    [SerializeField] Transform[] vertices;
    float phase;
    [SerializeField] private float amp1,amp2,len1,len2;

    private void Start()
    {
        for (int i = 0; i < vertices.Length; i++)
        {
            vertices[i].position = new Vector3(i, vertices[i].position.y, vertices[i].position.z);
        }
    }

    private void Update()
    {
        phase += Time.deltaTime;
       

        
        float x = phase;

       

        for (int i = 0; i < vertices.Length; i++)
        {
            //  float y = 0;
            //  amp1 = .5f;
            //  len1 = 4;
            //  x -= amp1 * Mathf.Sin(phase-i / len1 - (phase - i) / Mathf.Sqrt(len1));
            //  y += amp1 * Mathf.Cos(phase-i / len1 - (phase - i) / Mathf.Sqrt(len1));

            //  amp2 = .25f;
            //  len2 = 2;
            // x -= amp2 * Mathf.Sin(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            // y += amp2 * Mathf.Cos(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            //  vertices[i].position = new Vector2(vertices[i].position.x, y);
            //^ Good sin wave

            //phase = Time.time - i;
            //float x1 = phase- amp1 * Mathf.Sin(phase);
            //float y1= amp1 * Mathf.Cos(phase);
            //vertices[i].position = new Vector2(x1, y1);
            // ^ Base Triochodial

        }


    }

    
}

//for (int i = 0; i < vertices.Length; i++)
//{

 //   vertices[i].position = new Vector2(vertices[i].position.x, Mathf.Sin(phase - i));

//}