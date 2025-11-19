using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackWave : MonoBehaviour
{
    [SerializeField] WavesWithLineRender waves;
    private LineRenderer line;


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = waves.linerender.positionCount;
    }

    private void Update()
    {
        for (int i = 0; i < line.positionCount; i++) 
        {
            line.SetPosition(i, new Vector3(waves.vertices[i].x, waves.vertices[i].y + .3f, waves.vertices[i].z));
        }
    }
}
