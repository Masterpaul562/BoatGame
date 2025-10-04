using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishManager fishManager;
    [SerializeField] private GameObject bobber;
    [SerializeField] private LineRenderController line;
    [SerializeField] private CastFishingLine castScript;
    [SerializeField] private FishInventory inventory;
    public bool fishHooked;




    //  private void Awake()
    //  {

    // }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && bobber.GetComponent<Bobber>().submerged && fishManager.canHook)
        {
            fishManager.canHook = false;
            fishHooked = true;
            if (fishManager.closestFish.GetComponent<Fish>().swimDirection == 1)
            {

                fishManager.closestFish.GetComponent<Fish>().shouldFlip = true;
                fishManager.closestFish.GetComponent<Fish>().Flip();
            }
        }
        if (fishHooked)
        {
            fishManager.canHook = false;
            ReelIn();
            fishManager.StopSwimmingToBobber();
        }
    }

    private void ReelIn()
    {
        Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
        bobber.transform.position = pos;
        
        fishManager.closestFish.transform.position = bobber.transform.position;
        fishManager.closestFish.transform.parent = bobber.transform;
        float distance = Vector2.Distance(transform.position, bobber.transform.position);
        if (distance < 1)
        {
            bobber.SetActive(false);
            line.gameObject.SetActive(false);
            bobber.GetComponent<Bobber>().rb.simulated = true;
            bobber.GetComponent<Bobber>().submerged = false;
            castScript.hasCast = false;
            fishHooked = false;
            castScript.canCast = true;
            SecureFish();
        }

    }
    private void SecureFish()
    {
        inventory.AddFishOutside(1);
        fishManager.fishList.maxNumOfFish += 10;
        Destroy(fishManager.closestFish);
        fishManager.fishList.fish.RemoveAt(fishManager.closestFishIndex);
       
    }

}
