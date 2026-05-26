using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun2 harpoon;

    public ParticleSystem blood;

    [SerializeField] private ParticleSystem bubble;

    public ParticleSystem bubbleSystem;

    
    
    
    private void OnCollisionEnter2D(Collision2D other)
    {
         if( other.gameObject.tag == "Inside" )
        {
            
            StopAllCoroutines();
            harpoon.StartReel();
        }

    }
    private void OnTriggerEnter2D (Collider2D other)
    {
        if( other.gameObject.tag == "Fish")
        {
            Debug.Log("HIT");
            StopAllCoroutines();
            Vector3 bloodPos = new Vector3(other.transform.position.x, other.transform.position.y, blood.transform.position.z); 
            var bloodClone = Instantiate(blood, bloodPos , Quaternion.Euler(0, 0, 0));
            bloodClone.gameObject.SetActive(true);
            //blood.transform.parent = null;
            //blood.Play();
            harpoon.StartReel();
        }
        if(other.gameObject.tag == "Water")
        {
            Bubbles();
        }

    }


    private void Bubbles()
    {
        
       bubbleSystem =Instantiate(bubble,bubble.transform.position,Quaternion.Euler(0,0,-37),transform);
       bubbleSystem.gameObject.SetActive(true);
    }
    public void DestroyBubble(){

        if(bubbleSystem != null){
            bubbleSystem.transform.parent = null;
            bubbleSystem.transform.localScale = new Vector3(2,2,2);
            bubbleSystem.Stop();
            //Destroy(bubbleSystem.gameObject);
        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
}
