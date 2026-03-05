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
   private float powerLevel;
   private float maxScale;
   private int powerStage = 3;
   private bool shouldDrain = true;
   private bool canFeed= true;

   
   private void Start()
   {
     inventory = player.GetComponent<FishInventory>();
     fishing = player.GetComponent<HarpoonGun>();
     maxScale = lightBar.transform.localScale.x;
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
    powerLevel = Mathf.MoveTowards(powerLevel,0,Time.deltaTime);
    if(powerLevel <= 0){
        powerLevel = 100;
        powerStage --;
        SetLightBar();
    }
    if(powerStage <= 0){
      shouldDrain = false;
      canFeed;
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

   }
   private void SetLightBar()
   {
    float scale;
      for (int i; i < powerStage;i++)
      {
        scale += maxScale/3;
      }
      lightBar.transform.localScale.x = scale;
   }
}
