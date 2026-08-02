using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInventory : MonoBehaviour
{
    public int fishAmountOutside;
    public int fishAmountInside;
    public int scrapAmount;
    public FishEngineReal feed;



    public void AddFishOutside(int amount)
    {
        fishAmountOutside += amount;

    }
    public void AddFishInside(int amount)
    {
        fishAmountInside += amount;

    }
    public void AddJunk(int amount)
    {
        scrapAmount += amount;
    }
    public int GetFishAmount(bool inside)
    {
        if (inside)
        {
            return fishAmountInside;
        }else 
        {
            return fishAmountOutside;
        }
       
    }
    private void Feed()
    {
        feed.FeedFish();
        feed.source.Play();
    }

}
