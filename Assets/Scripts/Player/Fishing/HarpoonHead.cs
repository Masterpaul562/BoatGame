using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun2 harpoon;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
         if( other.gameObject.tag == "Inside")
        {
            StopAllCoroutines();
            StartCoroutine(harpoon.Reel());
        }

    }
}
