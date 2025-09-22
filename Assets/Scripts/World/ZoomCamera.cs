using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] private Camera cam;
    public float ogZoom;
    public float targetZoom;
   

    void Start()
    {
        ogZoom = cam.orthographicSize;
    }

    public void ZoomCam(bool zoom)
    {
       
        if (zoom)
        {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, Time.deltaTime*10);                        
        }
        else if (!zoom)
        {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, ogZoom, Time.deltaTime*10);                                      
        }
      
    }
}
