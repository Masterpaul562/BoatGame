using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedWaveWithLine : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LineRenderer line;
    private float[] phasePerWave;
    private float phase;
    private float amp1, amp2, len1, len2;

    private void Start()
    {
        line = GetComponent<LineRenderer>();
        
    }

    private void Update()
    {
        SetLocation();
        MoveWave();
        
    }

    private void SetLocation()
    {
        if (cam != null)
        {
            transform.position = new Vector3(cam.GetComponent<CamSizeManager>().worldWidth/2,transform.position.y,transform.position.z);
        }

    }
    private void MoveWave()
    {
        phasePerWave = new float[line.positionCount];
        phase += Time.deltaTime;
       
        for (int i = 0; i < line.positionCount; i++)
        {
            float y = 0;
            float x = phase;
            phasePerWave[i] = phase;
            float amp1 = this.amp1;
            float len1 = this.len1;
            x -= amp1 * Mathf.Sin(phase / len1 - (Time.time - i) / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase - i / len1 - (Time.time - i) / Mathf.Sqrt(len1));
            
        }

    }
    private IEnumerator SpawnWave()
    {
        yield return null;
    }
}
