using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEngineReal : MonoBehaviour
{
   [SerializeField] private GameObject player;
   [SerializeField] private LayerMask interactable;
   [SerializeField] private GameObject lightBar;
   private HarpoonGun fishing;
   private FishInventory inventory;
   [SerializeField] private float powerLevel;
   [SerializeField] private float maxScale;
   [SerializeField] private int powerStage = 3;
   [SerializeField] private bool shouldDrain = true;
   [SerializeField] private bool canFeed= true;
    public float drainSpeed; 



   private void Start()
   {
     inventory = player.GetComponent<FishInventory>();
     fishing = player.GetComponent<HarpoonGun>();
     maxScale = lightBar.transform.localScale.x;
        powerLevel = 100;
     SetLightBar();
   }
   private void Update()
   {

    float vert = Input.GetAxisRaw("Vertical");
    if(vert<0 && !fishing.isFishing){
        Interact();
    }


    if(shouldDrain){
    DrainPower();
    }
   }
   private void DrainPower()
   {
    powerLevel = Mathf.MoveTowards(powerLevel,0,Time.deltaTime * drainSpeed);
    if(powerLevel <= 0){
        powerLevel = 100;
        powerStage --;
        SetLightBar();
    }
    if(powerStage <= 0){
      shouldDrain = false;
      canFeed = false;
      StartCoroutine(Blink());
    }
   }
   private void Interact()
   {
       RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.forward, 10, interactable);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Engine" && inventory.fishAmountOutside != 0 && canFeed)
            {
                FeedFish();
            }
        }
   }
   private IEnumerator Blink(){
        yield return null;
   }
   private void SetLightBar()
   {
    float scale = 0;
      for (int i = 0; i < powerStage;i++)
      {
        scale += maxScale/3;
      }
      lightBar.transform.localScale = new Vector3(scale, lightBar.transform.localScale.y, lightBar.transform.localScale.z);
   }
    private void FeedFish()
    {

    }
}
