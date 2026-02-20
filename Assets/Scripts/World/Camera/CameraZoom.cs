using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
   private Camera cam;
   public float ogZoom; // Pre zoom size
   public Vector3 ogPosition; // Pre zoom position
   public float targetZoom; // Target zoom size
   public Transform targetPosition; // Target zoom position
   public float speedZoom; // Speed of zoom
    public float moveSpeed;


  private void Start() 
  {           
        cam = GetComponent<Camera>();
        ogZoom = cam.orthographicSize;
        ogPosition = cam.transform.position;
        targetZoom = cam.orthographicSize;
        targetPosition = cam.transform;
  }
  private void Update() {
    Zoom();
  }

    private void Zoom()
    {
        //ogZoom = cam.orthographicSize;
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, Time.deltaTime*speedZoom);
        if( targetPosition != null){
        cam.transform.position = Vector3.MoveTowards(cam.transform.position, targetPosition.position, moveSpeed); 
        }
    }

    
}
