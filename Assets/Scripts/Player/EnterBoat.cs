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
    [SerializeField] private SpriteRenderer insideBG;
    [SerializeField] private CityManager inCity;
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private string insideLayer, outsideLayer;
    [SerializeField] private Animator animator;
    [SerializeField] private Animator doorAnim;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject rain;
    [SerializeField] private GameObject waves;
    public bool canEnter = true;
    private bool EnterCd = true;
    private float alpha = 120;
    public bool inBoat;
    
    void Start()
    {
        Player = this.gameObject;
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
        cam.GetComponent<CameraZoom>().targetZoom = 4.7f;
        cam.GetComponent<CameraZoom>().targetPosition.position = new Vector3 (cam.transform.position.x, cam.transform.position.y - 2, cam.transform.position.z);
        //insideBG.enabled = true;
        doorAnim.SetTrigger("Open");
        this.GetComponent<PlayerMove>().freeze = true;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
        yield return new WaitForSeconds(.5f);
        waves.SetActive(false);
        rain.SetActive(false);
        this.GetComponent<PlayerMove>().freeze = false;
        inBoat = true;
        Player.GetComponent<SpriteRenderer>().sortingLayerName= insideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = insideLayer;
        animator.SetBool("IsInside", true);
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
        boatCollider.SetActive(false);
        boatInside.SetActive(true);
        boatInsideCollider.SetActive(true);
        if (inCity.inCity)
        {
            inCity.justEnteredCity = false;
            inCity.shouldZoom = false;
        }
        yield return null;
    }
    private void Exit()
    {
        rain.SetActive(true);
        waves.SetActive(true);
        EnterCd = true;
        doorAnim.SetBool("Open", false);
        doorAnim.SetTrigger("Close");
        inBoat = false;
        Player.GetComponent<SpriteRenderer>().sortingLayerName= outsideLayer;
        harpoon.GetComponent<SpriteRenderer>().sortingLayerName = outsideLayer;
        animator.SetBool("IsInside", false);
        insideBG.color = new Color(0, 0, 0, alpha);
        transform.position = exitLocation.position;
        boatCollider.SetActive(true);
        boatInside.SetActive(false);
        boatInsideCollider.SetActive(false);
        inCity.shouldZoom = true;        
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
   // private void FadeBG(float change)
   
            
            //insideBG.color = new Color(0, 0, 0, change/255);
     
}
