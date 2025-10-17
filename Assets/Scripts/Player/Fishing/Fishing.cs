using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fishing : MonoBehaviour
{
    [SerializeField] private FishManager fishManager;
    [SerializeField] private JunkManager junkManager;
    [SerializeField] private GameObject bobber;
    [SerializeField] private LineRenderController line;
    [SerializeField] private CastFishingLine castScript;
    [SerializeField] private FishInventory inventory;
    public bool fishHooked;
    public bool junkHooked;
    public bool securingItem;




    //  private void Awake()
    //  {

    // }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Z) && bobber.GetComponent<Bobber>().submerged )
        {
            if (fishManager.canHook)
            {
                fishManager.canHook = false;
                fishHooked = true;
                if (fishManager.closestFish.GetComponent<Fish>().swimDirection == 1)
                {

                    fishManager.closestFish.GetComponent<Fish>().shouldFlip = true;
                    fishManager.closestFish.GetComponent<Fish>().Flip();
                }
            }else if (junkManager.canHook)
            {
                junkManager.canHook = false;
                junkHooked = true;
            }
        }
        if (fishHooked)
        {
            fishManager.canHook = false;
            ReelInFish();
            fishManager.StopSwimmingToBobber();
        } else if (junkHooked)
        {
            junkManager.canHook = false;
            ReelInJunk();

        }
    }

    private void ReelInJunk()
    {
        securingItem = true;
        bobber.GetComponent<Bobber>().submerged = false;
        Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
        bobber.transform.position = pos;
        junkManager.hookedJunk.transform.position = bobber.transform.position;
        junkManager.hookedJunk.transform.parent = bobber.transform;
        float dist = Vector2.Distance(transform.position, bobber.transform.position);
        if (dist < 1)
        {
            bobber.SetActive(false);
            line.gameObject.SetActive(false);
            bobber.GetComponent<Bobber>().rb.simulated = true;
            bobber.GetComponent<Bobber>().submerged = false;
            castScript.hasCast = false;
            junkHooked = false;
            castScript.canCast = true;
            SecureJunk();

        }
    }
    private void ReelInFish()
    {
        securingItem = true;
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
        securingItem = false;

    }
    private void SecureJunk()
    {
        inventory.AddJunk(1);
    
        Destroy(junkManager.hookedJunk);
        junkManager.junkList.junk.RemoveAt(junkManager.hookedJunkIndex);
        securingItem = false;

    }

}
