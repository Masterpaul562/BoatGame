using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun2 harpoon;
    public ParticleSystem blood;
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
         if( other.gameObject.tag == "Inside" )
        {
            
            StopAllCoroutines();
            StartCoroutine(harpoon.Reel());
        }

    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if( other.gameObject.tag == "Fish")
        {
            Debug.Log("HIT");
            StopAllCoroutines();
            blood.transform.parent = null;
            blood.Play();
            StartCoroutine(harpoon.Reel());
        }
    }
}
