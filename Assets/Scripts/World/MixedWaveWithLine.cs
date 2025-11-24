using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MixedWaveWithLine : MonoBehaviour
{
    [SerializeField] Camera cam;
    [SerializeField] LineRenderer line;
    [SerializeField] private float[] phasePer;
    private float phase;
    private float phaseOffset;
    [SerializeField] private float amp1, amp2, len1, len2;
    [SerializeField] private Vector3 point;
    public float closestPoint;
    [SerializeField] Transform center;

    


    private void Start()
    {
        line = GetComponent<LineRenderer>();
        phasePer = new float[20];
    }

    private void Update()
    {
        SetLocation();
        MoveWave();

        point = cam.WorldToViewportPoint(line.GetPosition(0));


        if (point.x < -0.5f)
        {
            Debug.Log("yay");
            DeleteWave();
        }
        findClosest();
       // Debug.Log(cam.WorldToViewportPoint(Input.mousePosition));

    }

    private void SetLocation()
    {
        if (cam != null)
        {
            transform.position = new Vector3((-cam.GetComponent<CamSizeManager>().worldWidth / 2)-10, transform.position.y, transform.position.z);
        }

    }
    private void MoveWave()
    {
        //line.positionCount = wavePos.Count;
        phasePer = new float[line.positionCount];
        for (int i = 0; i < line.positionCount; i++)
        {
            
            phase = Time.time - i- phaseOffset;
            phasePer[i] = phase;
            float x = phase;
            float y = 0;
            x -= phase - amp1 * Mathf.Sin(phase / len1 - Time.time / Mathf.Sqrt(len1));
            y += amp1 * Mathf.Cos(phase / len1 - Time.time / Mathf.Sqrt(len1));
            x -= phase - amp2 * Mathf.Sin(phase / len1 - Time.time / Mathf.Sqrt(len2));
            y += amp2 * Mathf.Cos(phase / len2 - Time.time / Mathf.Sqrt(len2));
            line.SetPosition(i, new Vector3(x, y, 1));
        }
       // phaseOffset = phase;
    }

    private void DeleteWave()
    {
        phaseOffset = Time.time;
    }
    private void findClosest()
    {
        float minDist = 10000;
        for (int i = 0;i < line.positionCount;i++)
        {
            float distance = Vector2.Distance(new Vector2 (line.GetPosition(i).x,0), new Vector2(-transform.position.x,0));
           // Debug.Log(distance);
            if (minDist > distance)
            {
                minDist = distance;
                closestPoint = line.GetPosition(i).y;
            }
        }
    }
}
