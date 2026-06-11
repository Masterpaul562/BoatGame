using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun2 harpoon;

    public ParticleSystem blood;

    [SerializeField] private ParticleSystem bubble;

    public ParticleSystem bubbleSystem;
    public float particleSize;
    public float bubbleOffsetY;
    public LayerMask water;
    public AudioSource source;

    
    
  
    
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
            source.Play();  
            StopAllCoroutines();
            Vector3 bloodPos = new Vector3(other.transform.position.x, other.transform.position.y, blood.transform.position.z); 
            var bloodClone = Instantiate(blood, bloodPos , Quaternion.Euler(0, 0, 0));
            bloodClone.gameObject.SetActive(true);
            harpoon.StartReel();
        }
       // if(other.gameObject.tag == "Water")
      //  {
          
           // Bubbles();
       // }

    }


    private void Bubbles()
    {

        Vector3 bubblePosition = new Vector3(0, 0, 0);
        Quaternion rotation = new Quaternion(0,0,0,0);
        if (transform.localScale.x > 0)
        {
             rotation = Quaternion.Euler(0, 0, harpoon.transform.GetChild(0).transform.eulerAngles.z);
            bubblePosition = new Vector3(bubble.transform.position.x + bubbleOffsetY, bubble.transform.position.y - bubbleOffsetY, bubble.transform.position.z);
        } else
        {
            rotation = Quaternion.Euler(0, 0, harpoon.transform.GetChild(0).transform.eulerAngles.z -180);
            bubblePosition = new Vector3(bubble.transform.position.x - bubbleOffsetY, bubble.transform.position.y - bubbleOffsetY, bubble.transform.position.z);
        }

       bubbleSystem =Instantiate(bubble,bubblePosition, rotation );
       bubbleSystem.transform.localScale = new Vector3(particleSize, particleSize, particleSize);
       bubbleSystem.gameObject.SetActive(true);
       
    }
    public void DestroyBubble(){

        if(bubbleSystem != null){
            bubbleSystem.transform.parent = null;
            //bubbleSystem.transform.localScale = new Vector3(particleSize,particleSize,particleSize);
            bubbleSystem.Stop();
            bubbleSystem = null;
            //Destroy(bubbleSystem.gameObject);
        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
}
