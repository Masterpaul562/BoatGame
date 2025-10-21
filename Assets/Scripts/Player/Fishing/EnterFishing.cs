using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterFishing : MonoBehaviour
{
    [SerializeField] private LayerMask interactable;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Camera cam;
    [SerializeField] public Transform fishingCamSpot;
    [SerializeField] private Transform ogPos;
    [SerializeField] private SpriteRenderer render;
    [SerializeField] private CastFishingLine castLineScript;
    [SerializeField] private BgScroller[] backGrounds;
    [SerializeField] private Fishing fishing;

    private Fishing fishingScript;
    public bool isFishing;



    private void Awake()
    {
        fishingScript = GetComponent<Fishing>();
        render = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        castLineScript = GetComponent<CastFishingLine>();
    }
    void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        if (vert < 0 && !isFishing)
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
        else if (vert > 0 && isFishing&&fishing.securingItem == false)
        {
            ExitFishing();
        }
    


    }
    private void FixedUpdate()
    {
        if (isFishing && cam.transform.position != fishingCamSpot.position)
        {

            Vector2 pos = Vector2.MoveTowards(cam.transform.position, fishingCamSpot.position, Time.deltaTime * 10);
            cam.transform.position = new Vector3(pos.x, pos.y, -10);
        }
        else if (!isFishing && cam.transform.position != ogPos.position)
        {
            Vector2 pos = Vector2.MoveTowards(cam.transform.position, ogPos.position, Time.deltaTime * 10);
            cam.transform.position = new Vector3(pos.x, pos.y, -10);
        }
    }
    private void StartFishing()
    {


        isFishing = true;
       
       

        //doo this to fishing menu/game
        //.SetActive(true) 

    }
    private void ExitFishing()
    {
        fishingScript.fishHooked = false;
        fishingScript.enabled = false;
        isFishing = false;
        
        if (castLineScript.hasCast)
        {
            castLineScript.shouldReel = true;
        }
        

    }
   

}
