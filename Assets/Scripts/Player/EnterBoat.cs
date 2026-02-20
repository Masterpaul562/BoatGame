using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterBoat : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;

    [Header("")]

    [SerializeField] private GameObject boatCollider; // outside colliders
    [SerializeField] private GameObject boatInside; // inside boat art BG
    [SerializeField] private GameObject boatInsideCollider; // Inside boat Colliders
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject waves;
    [SerializeField] private GameObject player; // Player Object
    [SerializeField] private GameObject harpoon; // Harpoon Object
    [SerializeField] private GameObject spray;

    [Header("")]
    [SerializeField] private Transform enterLocation; // Locations for enter and exit 
    [SerializeField] private Transform exitLocation;
    [SerializeField] private SpriteRenderer insideBG; // Black BG for inside

    [SerializeField] private string insideLayer, outsideLayer; // String to change layers 

    [SerializeField] private Animator animator; // 
    [SerializeField] private Animator doorAnim; // animator of the door

    [Header("Zoom Settings")]
    [SerializeField] private Camera cam;
    [SerializeField] private float insideZoom;
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float zoomSpeed;

    [Header("")]
    public bool canEnter = true;
    public bool inBoat;
    private bool EnterCd = true;
    private float alpha = 223;
  
    
    void Start()
    {
        player = this.gameObject;
        animator = GetComponent<Animator>();
    }


    void Update()
    {
       
        float vert = Input.GetAxisRaw("Vertical");
        
            // alpha = Mathf.Lerp(alpha, 255, Time.deltaTime*20);
          //  FadeBG(alpha);         
        if (vert < 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10,interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Enter"&&canEnter && EnterCd)
                {
                    
                    StartCoroutine(Enter());
                   
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
    private IEnumerator Enter()
    {
        EnterCd = false;
        

        // Zoom and move camera when entering boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = insideZoom;
        zoom.targetPosition.position = new Vector3 (cam.transform.position.x, boatInside.transform.position.y, cam.transform.position.z);
        zoom.speedZoom = zoomSpeed;
        

        //Play animation for door opening
        doorAnim.SetTrigger("Open");
        this.GetComponent<PlayerMove>().freeze = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);

        yield return new WaitForSeconds(.5f);

        this.GetComponent<PlayerMove>().freeze = false;

        //Set stuff active/inactive for inside        
        StartCoroutine(FadeBG(true));
        waves.SetActive(false);
        rain.SetActive(false);
        inBoat = true;
        animator.SetBool("IsInside", true);
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);

        // change layers to display correctly
        player.GetComponent<SpriteRenderer>().sortingLayerName= insideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = insideLayer;

        // Set Position and make sure character is facing right
        transform.position = enterLocation.position;
        Vector3 localScale = transform.localScale;
        if (localScale.x < 0)
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;


        animator.SetBool("isFacingRight", true);
        animator.SetBool("Turn", false);
        this.GetComponent<PlayerMove>().isFacingRight = true;

        yield return null;
    }
    private void Exit()
    {
        // Zoom and move camera when exiting boat. 
        var zoom = cam.GetComponent<CameraZoom>();
        zoom.targetZoom = zoom.ogZoom;
        zoom.targetPosition.position = zoom.ogPosition;
        zoom.moveSpeed = 2;

        zoom.speedZoom = zoomSpeed/2;
        StartCoroutine(FadeBG(false));


        //Set stuff active/inactive for outside
        EnterCd = true;
        inBoat = false;
        animator.SetBool("IsInside", false);
        rain.SetActive(true);
        waves.SetActive(true);
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);

        //Play door animation
        doorAnim.SetBool("Open", false);
        doorAnim.SetTrigger("Close");
        
        //Change layer
        player.GetComponent<SpriteRenderer>().sortingLayerName= outsideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = outsideLayer;
      
       // insideBG.color = new Color(0, 0, 0, alpha);

        // move outside and make character face left
        transform.position = exitLocation.position;
        Vector3 localScale = transform.localScale;
        if (localScale.x > 0)
        {
            localScale.x *= -1f;
        }
        transform.localScale = localScale;

        animator.SetBool("isFacingRight", false);
        animator.SetBool("Turn", false);
        this.GetComponent<PlayerMove>().isFacingRight = false;

    }
    private IEnumerator FadeBG(bool black)
    {
        if (black)
        {
            insideBG.enabled = true;
            while (alpha != 255)
            {
                alpha = Mathf.MoveTowards(alpha, 255, Time.deltaTime * fadeSpeed);
                insideBG.color = new Color(0, 0, 0, alpha / 255);
                yield return null;
            }
        }else
        if (!black)
        {
            while (alpha != 223)
            {
                alpha = Mathf.MoveTowards(alpha, 223, Time.deltaTime * fadeSpeed);
                insideBG.color = new Color(0, 0, 0, alpha / 255);
                yield return null;
            }
            insideBG.enabled = false;
        }

    }
            
            //insideBG.color = new Color(0, 0, 0, change/255);
     
}
