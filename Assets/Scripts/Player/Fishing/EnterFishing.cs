using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFishing : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] private Transform fishingCamSpot;
    [SerializeField] private Transform ogPos;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private CastFishingLine castLineScript;
    public bool isFishing;


    private void Awake()
    {
       render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
       castLineScript = GetComponent<CastFishingLine>();
    }
    void Update()
    {
        // float vert = Input.GetAxisRaw("Vertical");
        if (Input.GetKeyDown(KeyCode.S))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, 10, interactable);
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.tag == "Fishing")
                {
                    StartFishing();
                }
            }
        }
        else if (Input.GetKeyDown(KeyCode.W) && isFishing)
        {
            ExitFishing();
        }
        
       

    }
    private void FixedUpdate()
    {
        if (isFishing && cam.transform.position != fishingCamSpot.position)
        { 
          
                Vector2 pos = Vector2.MoveTowards(cam.transform.position, fishingCamSpot.position, Time.deltaTime*10);
            cam.transform.position = new Vector3(pos.x,pos.y,-10);
        }else if (!isFishing && cam.transform.position != ogPos.position) {
            Vector2 pos = Vector2.MoveTowards(cam.transform.position, ogPos.position, Time.deltaTime * 10);
            cam.transform.position = new Vector3(pos.x, pos.y, -10);
        }
    }
    private void StartFishing()
    {
        
       
        isFishing = true;
        render.flipX = true;
        //castLineScript.bobber.GetComponent<Bobber>().rb.simulated = true;

        //doo this to fishing menu/game
       //.SetActive(true) 
       
    }
    private void ExitFishing()
    {
      
        isFishing = false;
        render.flipX = false;
       // castLineScript.hasCast = false;
        //castLineScript.bobber.GetComponent<Bobber>().rb.simulated = true;
    }

}
