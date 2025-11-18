using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WavesWithLineRender : MonoBehaviour
{
    [SerializeField] Vector3[] vertices;
    [SerializeField] LineRenderer linerender;
    float phase;
    [SerializeField] private float amp1, amp2, len1, len2;

    private void Start()
    {
        linerender = GetComponent<LineRenderer>();
        vertices = new Vector3[linerender.positionCount];
        for (int i = 0; i < vertices.Length; i++)
        {
            // vertices[i].position = new Vector3(i, vertices[i].position.y, vertices[i].position.z);
            vertices[i] = linerender.GetPosition(i);
            vertices[i].x = vertices[i].x *-1;
        }              
        //vertices = linerender.GetPositions(out Vector3[], linerender.positionCount);
    }

    private void Update()
    {
        phase += Time.deltaTime;


        
        float x = phase;



        for (int i = 0; i < vertices.Length; i++)
        {
            float y = 0;
            float amp1 = this.amp1;
            float len1 = this.len1;
            x -= amp1 * Mathf.Sin(phase / len1 - (phase - i) / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase / len1 - (phase - i) / Mathf.Sqrt(len1));

            float amp2 = this.amp2;
            float len2 = this.len2;
            x -= amp2 * Mathf.Sin(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            y += amp2 * Mathf.Cos(phase / len2 - (phase - i) / Mathf.Sqrt(len2));
            vertices[i] = new Vector2(vertices[i].x, -1*y);
            linerender.SetPosition( i, vertices[i]);
        }


    }
}
