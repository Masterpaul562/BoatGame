using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VertIceTest : MonoBehaviour
{
    [SerializeField] Transform[] vertices;
    float phase;
    private float amp1,amp2,len1,len2;

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
            float y = 0;
            amp1 = .5f;
            len1 = 4;
            x -= amp1 * Mathf.Sin(phase / len1 - (phase - i) / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase / len1 - (phase - i) / Mathf.Sqrt(len1));

            amp2 = .25f;
            len2 = 2;
            x -= amp2 * Mathf.Sin(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            y += amp2 * Mathf.Cos(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            vertices[i].position = new Vector2(vertices[i].position.x, y);

        }


    }

    
}

//for (int i = 0; i < vertices.Length; i++)
//{

 //   vertices[i].position = new Vector2(vertices[i].position.x, Mathf.Sin(phase - i));

//}