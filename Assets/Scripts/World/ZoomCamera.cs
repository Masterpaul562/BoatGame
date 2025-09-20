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

    public IEnumerator ZoomCam(string zoom)
    {
       
        if (zoom == "1")
        {
            cam.orthographicSize = ogZoom;
            while (cam.orthographicSize != targetZoom)
            {
               
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, Time.deltaTime*10);
                Debug.Log("Yippe");
                yield return null;
            }
        }
        else if (zoom == "0")
        {
            cam.orthographicSize = targetZoom;
            while (cam.orthographicSize != ogZoom)
            {
                Debug.Log("yay");
                cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, ogZoom, Time.deltaTime*10);
               
                yield return null;
            }
        }
      
    }
}
