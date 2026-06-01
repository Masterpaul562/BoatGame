using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraZoom : MonoBehaviour
{
   private Camera cam;
   public Camera secondaryCam;
   [Header("Zoom Settings")]
   public float ogZoom; // Pre zoom size
   public Vector3 ogPosition; // Pre zoom position
   public float targetZoom; // Target zoom size
   public Vector3 targetPosition; // Target zoom position
   public float zoomSpeed;
   [Header("Fade Settings")]
   [SerializeField] private SpriteRenderer insideBG;// the sprite renderer for the Black BG for inside
   private float alpha;

   



  private void Awake() 
  {           
        cam = GetComponent<Camera>();
        ogZoom = cam.orthographicSize;
        ogPosition = cam.transform.position;
        targetZoom = cam.orthographicSize;
        targetPosition = cam.transform.position;
  }
  private void Update() {

    Zoom();
  }

    private void Zoom()
    {
        //ogZoom = cam.orthographicSize;
        cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, Time.deltaTime*Mathf.Abs(cam.orthographicSize - targetZoom)*zoomSpeed);
        if( targetPosition != null){
            float y = Mathf.MoveTowards(transform.position.y, targetPosition.y, Time.deltaTime* Mathf.Abs(transform.position.y-targetPosition.y)*zoomSpeed);
            cam.transform.position = new Vector3(transform.position.x, y, transform.position.z);
        }
        secondaryCam.orthographicSize = cam.orthographicSize;
        secondaryCam.transform.position = cam.transform.position;
    }

    public IEnumerator FadeBG(bool black, float speed)
    {
        if (black)
        {
            insideBG.enabled = true;
            alpha = 223f;
            //insideBG.color = new Color(0, 0, 0, 223/255);
            Debug.Log("BLack");
            while (alpha != 255)
            {
                alpha = Mathf.MoveTowards(alpha, 255, Time.deltaTime * speed);
                insideBG.color = new Color(0, 0, 0, alpha / 255);
                yield return null;
            }
        }else
        if (!black)
        {
            while (alpha != 0)
            {
                alpha = Mathf.MoveTowards(alpha, 0, Time.deltaTime * speed);
                insideBG.color = new Color(0, 0, 0, alpha / 255);
                yield return null;
            }
            insideBG.enabled = false;
           // insideBG.color = new Color(0, 0, 0, 223);
        }

    }
    
}
