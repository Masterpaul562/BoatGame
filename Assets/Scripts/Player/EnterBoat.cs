using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private GameObject boatCollider;
    [SerializeField] private GameObject boatInside;
    [SerializeField] private GameObject boatInsideCollider;
    [SerializeField] private Transform enterLocation;
    [SerializeField] private Transform exitLocation;
    [SerializeField] private ZoomCamera camZoom;
    [SerializeField] private SpriteRenderer insideBG;
    public bool zoom;
    private float alpha = 120;
    
    void Start()
    {
        camZoom = GetComponent<ZoomCamera>();
    }


    void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        camZoom.ZoomCam(zoom);
        if ( zoom)
        {
             alpha = Mathf.Lerp(alpha, 255, Time.deltaTime*20);
            FadeBG(alpha);
        } else if (!zoom)
        {
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime *10 );
            FadeBG(alpha);
          //  if (alpha <= 10)
          //  {
               // insideBG.enabled = false;
          //  }
        }
        
        
        if (vert < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10,interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enter")
                {
                    Enter();
                }
            }
        }
        if (vert >0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Exit")
                {
                    Exit();
                }
            }
        }
    }
    private void Enter()
    {
        zoom = true;
       // alpha = 120;
        insideBG.enabled = true;
        transform.position = enterLocation.position;
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
        
    }
    private void Exit()
    {
       // alpha = 255;
        insideBG.color = new Color(0, 0, 0, alpha);
        zoom = false;
        transform.position = exitLocation.position;
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);
        
    }
    private void FadeBG(float change)
    {
            
            insideBG.color = new Color(0, 0, 0, change/255);
            //Debug.Log(change);
    }
}
