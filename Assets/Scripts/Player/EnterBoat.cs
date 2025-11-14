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
    [SerializeField] private CityManager inCity;
    [SerializeField] private GameObject Player;
    [SerializeField] private string insideLayer,outsideLayer;
    [SerializeField] private Animator animator;
    public bool shouldZoom;
    public bool zoom;
    public bool canEnter = true;
    private float alpha = 120;
    public bool inBoat;
    
    void Start()
    {
        camZoom = GetComponent<ZoomCamera>();
        Player = this.gameObject;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        if (inCity.inCity == false|| zoom)
        {
            if (shouldZoom||zoom)
            {
                camZoom.ZoomCam(zoom);
                
            }
        }
        if ( zoom)
        {
             alpha = Mathf.Lerp(alpha, 255, Time.deltaTime*20);
            FadeBG(alpha);
        } else if (!zoom)
        {
            alpha = Mathf.Lerp(alpha, 0, Time.deltaTime *10 );
            FadeBG(alpha);
            if(camZoom.cam.orthographicSize == camZoom.ogZoom)
            {
                shouldZoom = false;
            }

        }
        
        
        if (vert < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10,interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enter"&&canEnter)
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
                if (hit.collider.gameObject.tag == "Exit"&&canEnter)
                {
                    Exit();
                }
            }
        }
    }
    private void Enter()
    {
        inBoat = true;
        zoom = true;
        shouldZoom = true;
        Player.GetComponent<SpriteRenderer>().sortingLayerName= insideLayer;
        animator.SetBool("IsInside", true);
        insideBG.enabled = true;
        transform.position = enterLocation.position;
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
        if (inCity.inCity)
        {
            inCity.justEnteredCity = false;
            inCity.shouldZoom = false;
        }
        
    }
    private void Exit()
    {
        inBoat = false;
        Player.GetComponent<SpriteRenderer>().sortingLayerName= outsideLayer;
        animator.SetBool("IsInside", false);
        insideBG.color = new Color(0, 0, 0, alpha);
        zoom = false;
        shouldZoom = true;
        transform.position = exitLocation.position;
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);
        inCity.shouldZoom = true;

    }
    private void FadeBG(float change)
    {
            
            insideBG.color = new Color(0, 0, 0, change/255);
            //Debug.Log(change);
    }
}
