using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishEngine : MonoBehaviour
{
    [SerializeField] private FishInventory inventory;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject lightBar;
    [SerializeField] private LayerMask interactable;
    private int powerLevel;




    private void Update()
    {
        float vert = Input.GetAxisRaw("Vertical");
        if(vert<0)
        {
            Interact();
        }
    }

    private void FeedFish()
    {
        //Add power based on fish before feeding
        //might need a cooldown
        inventory.FeedFish();
    }
    private void Interact()
    {
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
