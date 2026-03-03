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
   private int powerStage;

   
   private void Start()
   {
     inventory = player.GetComponent<FishInventory>();
     fishing = player.GetComponent<HarpoonGun>();
     maxScale = lightBar.transform.localScale.x;
   }
   private void Update()
   {

    float vert = Input.GetAxisRaw("Vertical");
    if(vert<0 && !fishing.isFishing){
        Interact();
    }

    DrainPower();
   }
   private void DrainPower()
   {
    powerLevel = Mathf.MoveTowards(powerLevel,0,Time.deltaTime);
    if(powerLevel <= 0){
        powerLevel = 0;
    }
   }
   private void Interact(){

   }
}
