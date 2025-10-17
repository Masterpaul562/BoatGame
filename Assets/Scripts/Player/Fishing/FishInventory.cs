using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishInventory : MonoBehaviour
{
    public int fishAmountOutside;
    public int fishAmountInside;
    public int junkAmount;



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
        junkAmount += amount;
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
}
