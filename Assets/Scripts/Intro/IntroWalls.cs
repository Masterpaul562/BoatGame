using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWalls : MonoBehaviour
{
    public int maxHitAmount;
    private int hitAmount;





    private void Update()
    {
        if (maxHitAmount <= hitAmount)
        {
            Destroy(this.gameObject);
        }
    }




   private void OnCollisionEnter2D (Collision2D other) 
    { 

        if( other.gameObject.tag == "Bobber")
        {
            hitAmount++;
        }
    }

}
