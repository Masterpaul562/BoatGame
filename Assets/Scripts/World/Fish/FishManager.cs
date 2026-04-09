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
    [SerializeField] private HarpoonGun2 isFishing;
    [SerializeField] private FishInventory inventory;
    private bool startCoVanity;
    private bool startCoReal;
    public bool inEvent;


    private void Start()
    {
        fishList.StartFishy();
    }

    void Update()
    {
        DestroyRealFish();
       // FindClosestFish();

    }





  //  private void FindClosestFish()
  //  {

  //      closestDistance = 10000000000;
 //       for (int i = 0; i < fishList.fish.Count; i++)
 //       {
//            float distance = Vector2.Distance(bobber.transform.position, fishList.fish[i].transform.position);
 //           if (closestDistance > distance)
 //           {
 ///               closestDistance = distance;
 //               closestFish = fishList.fish[i];
//                closestFishIndex = i;
 //           }
//
     //   }


  //  }

  
    private void DestroyRealFish()
    {
        for (int i = 0; i < fishList.fish.Count; i++)
        {
            if (fishList.fish[i].GetComponent<Fish>().DestroyCheck())
            {
                Destroy(fishList.fish[i]);
                fishList.fish.RemoveAt(i);

            }
        }
    }   

    public void SecureFish(GameObject fish)
    {
        for (int i = 0; i < fishList.fish.Count; i++)
        {
            if (fish == fishList.fish[i])
            {
                Destroy(fishList.fish[i]);
                fishList.fish.RemoveAt(i);
                inventory.AddFishOutside(1);
            }
        }
       
    }
}
