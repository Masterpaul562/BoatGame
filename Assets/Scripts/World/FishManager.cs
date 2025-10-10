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
    [SerializeField] private EnterFishing isFishing;
    public bool canHook;


  

    void Update()
    {
        DestroyVanityFish();
        if (bobber.submerged)
        {
            vanityFish.shouldBeSpawning = false;
            FindClosestFish();
            CanHookCheck();
        }else
        {
            vanityFish.shouldBeSpawning = true;
        }
        DestroyRealFish();
       
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
            if (fishList.fish[i].GetComponent<Fish>().shouldBeDestroyed)
            {
                fishList.maxNumOfFish--;
                Destroy(fishList.fish[i]);
                fishList.fish.RemoveAt(i);

            }
        }
    }
    private void DestroyVanityFish()
    {

        for(int i = 0; i < vanityFish.fish.Count; i++)
        {
            if(vanityFish.fish[i].transform.position.x>7&&isFishing.isFishing&&vanityFish.fish.Count!=0){
                Destroy(vanityFish.fish[i]);
                vanityFish.fish.RemoveAt(i);
            }
    
            if (vanityFish.fish[i].GetComponent<FloaterMovement>().DestroyThis())
            {
                Destroy(vanityFish.fish[i]);
                vanityFish.fish.RemoveAt(i);

            }
        }
    }

}
