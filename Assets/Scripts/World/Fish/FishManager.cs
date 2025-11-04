using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishManager : MonoBehaviour
{

    [SerializeField] float closestDistance;
    [SerializeField] public GameObject closestFish;
    [SerializeField] public int closestFishIndex;
    [SerializeField] private Bobber bobber;
    [SerializeField] public FishSpawner fishList;
    [SerializeField] private VanityFishSpawner vanityFish;
    [SerializeField] private HarpoonGun isFishing;
    [SerializeField] private CityManager city;
    public bool canHook;
    private bool startCoVanity;
    private bool startCoReal;
    public bool inEvent;




    void Update()
    {
        DestroyVanityFish();
        DestroyRealFish();
        if (bobber.submerged)
        {
            FindClosestFish();
            CanHookCheck();
            if (inEvent == false)
            {
                if (startCoReal)
                {
                    startCoReal = false;
                    fishList.StartFishy();
                }
            }
        }
        else if (isFishing.isFishing == false)
        {

            MoveRealFishOff();
            canHook = false;
            fishList.shouldBeSpawning = false;
            startCoReal = true;
            vanityFish.enabled = true;
            if (startCoVanity && inEvent == false)
            {
                startCoVanity = false;
                vanityFish.maxNumOfFish = 7;
                vanityFish.Start();
            }
        }
        else //you are fishing but not in cast
        {
            startCoVanity = true;
            vanityFish.enabled = false;
            vanityFish.shouldBeSpawning = false;

        }
        if (inEvent)
        {
            
            
            vanityFish.shouldBeSpawning = false;
            fishList.shouldBeSpawning = false;
            startCoVanity = true;
            startCoReal = true;
        }



    }





    private void FindClosestFish()
    {

        closestDistance = 10000000000;
        for (int i = 0; i < fishList.fish.Count; i++)
        {
            float distance = Vector2.Distance(bobber.transform.position, fishList.fish[i].transform.position);
            if (closestDistance > distance)
            {
                closestDistance = distance;
                closestFish = fishList.fish[i];
                closestFishIndex = i;
            }

        }


    }

    private void CanHookCheck()
    {
        if (closestFish != null)
        {
            if (Vector2.Distance(bobber.transform.position, closestFish.transform.position) < 1)
            {
                canHook = true;

            }
        }



    }
    public void StopSwimmingToBobber()
    {
        for (int i = 0; i < fishList.fish.Count; i++)
        {

            fishList.fish[i].GetComponent<Fish>().shouldSwimToBobber = false;

        }
    }
    private void DestroyRealFish()
    {
        for (int i = 0; i < fishList.fish.Count; i++)
        {
            if (fishList.fish[i].GetComponent<Fish>().DestroyCheck())
            {
                fishList.maxNumOfFish--;
                Destroy(fishList.fish[i]);
                fishList.fish.RemoveAt(i);

            }
        }
    }
    private void DestroyVanityFish()
    {


        for (int i = 0; i < vanityFish.fish.Count; i++)
        {
            if (vanityFish.fish[i].GetComponent<FloaterMovement>().DestroyThis())
            {
                Destroy(vanityFish.fish[i]);
                vanityFish.fish.RemoveAt(i);

            }
        }
    }
    private void MoveRealFishOff()
    {
        for (int i = 0; i < fishList.fish.Count; i++)
        {

            fishList.fish[i].GetComponent<FloaterMovement>().enabled = true;
            fishList.fish[i].GetComponent<Fish>().enabled = false;
            if (fishList.fish[i].GetComponent<FloaterMovement>().DestroyThis())
            {
                Destroy(fishList.fish[i]);
                fishList.fish.RemoveAt(i);
            }

        }
    }
    public void MoveVanityFishOff()
    {
        for (int i = 0; i < vanityFish.fish.Count; i++)
        {

            vanityFish.fish[i].GetComponent<FloaterMovement>().moveAmount = 0.2f;
            vanityFish.maxNumOfFish = 0;
            if (vanityFish.fish[i].GetComponent<FloaterMovement>().DestroyThis())
            {
                Destroy(vanityFish.fish[i]);
                vanityFish.fish.RemoveAt(i);
            }

        }
    }

}
