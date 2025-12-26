using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoomCamera : MonoBehaviour
{
    [SerializeField] public Camera cam;
    public float ogZoom;
    public float targetZoom;
    [SerializeField] Transform insidePos;
   

    void Start()
    {
        ogZoom = cam.orthographicSize;
        insidePos.position = new Vector3(cam.transform.position.x,cam.transform.position.y-3,cam.transform.position.z);
    }

    public void ZoomCam(bool zoom)
    {
       
        if (zoom)
        {
            
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, Time.deltaTime*10);
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, insidePos.position, Time.deltaTime*5);                             
        }
        else if (!zoom)
        {
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, ogZoom, Time.deltaTime*10);                                      
        }
      
    }
}
