using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedWaveWithLine : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LineRenderer line;
    private float[] phasePerWave;
    private float phase;
    [SerializeField] private float amp1, amp2, len1, len2;

    private void Start()
    {
        line = GetComponent<LineRenderer>();

    }

    private void Update()
    {
        SetLocation();
        MoveWave();
        DeleteWave();

    }

    private void SetLocation()
    {
        if (cam != null)
        {
            transform.position = new Vector3(cam.GetComponent<CamSizeManager>().worldWidth / 2, transform.position.y, transform.position.z);
        }

    }
    private void MoveWave()
    {

      
        for (int i = 0; i < line.positionCount; i++)
        {

            phase = Time.time - i;
            float x = phase;
            float y = 0;
            x -= phase - amp1 * Mathf.Sin(phase / len1 - Time.time / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase/len1 -Time.time / Mathf.Sqrt(len1));
            x -= phase - amp2 * Mathf.Sin(phase / len1 - Time.time / Mathf.Sqrt(len2));
            y += amp2 * Mathf.Cos(phase / len2 - Time.time / Mathf.Sqrt(len2));
            line.SetPosition(i, new Vector3(x, y, 1));
        }

    }
    private IEnumerator SpawnWave()
    {
        yield return null;
    }
    private void DeleteWave()
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            Vector3 point = cam.WorldToViewportPoint(line.GetPosition(i));
            if( point.x < -0.01f)
            {
               // line.positionCount--;
                Debug.Log(i);
            }
        }
    }
}
