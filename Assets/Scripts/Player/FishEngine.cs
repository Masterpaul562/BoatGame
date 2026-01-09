using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEngine : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lightBar;
    [SerializeField] private LayerMask interactable;
    private HarpoonGun fishing;
    private FishInventory inventory;
    private float powerLevel;
    private float maxPowerLevel;

    private void Start()
    {
        inventory = player.GetComponent<FishInventory>();
        fishing = player.GetComponent<HarpoonGun>();
        maxPowerLevel = 1.6f;
        powerLevel = maxPowerLevel;
    }


    private void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        if(vert<0 && !fishing.isFishing)
        {
            Interact();
        }
    }

    private void FeedFish()
    {
        //Add power based on fish before feeding
        //might need a cooldown
        for(int i=0; i<inventory.fishAmountOutside; i++)
        {

        }
        inventory.fishAmountOutside = 0;
    }
    private void Interact()
    {
        Debug.Log("interact");
        RaycastHit2D hit = Physics2D.Raycast(player.transform.position, Vector3.forward, 10, interactable);
        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "Engine")
            {
                FeedFish();
                Debug.Log("Yay");
            }
        }
    }
    private void DrainPower()
    {

    }
}
