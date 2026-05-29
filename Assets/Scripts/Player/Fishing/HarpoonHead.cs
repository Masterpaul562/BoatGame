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
            var shape = bubble.shape;
            Debug.Log(harpoon.transform.GetChild(0).transform.localRotation.z);
            //Figure out if this is because they are not euler
            shape.rotation = new Vector3 (harpoon.transform.GetChild(0).transform.localRotation.z, bubble.shape.rotation.y, bubble.shape.rotation.z);
            Bubbles();
        }

    }


    private void Bubbles()
    {
        
       Vector3 bubblePosition = new Vector3(bubble.transform.position.x, bubble.transform.position.y - bubbleOffsetY, bubble.transform.position.z);
       Quaternion rotation = Quaternion.Euler(harpoon.transform.GetChild(0).transform.rotation.x, harpoon.transform.GetChild(0).transform.rotation.y, harpoon.transform.GetChild(0).transform.rotation.z * -1);

       bubbleSystem =Instantiate(bubble,bubblePosition, rotation );
       bubbleSystem.transform.localScale = new Vector3(particleSize, particleSize, particleSize);
       bubbleSystem.gameObject.SetActive(true);
       
    }
    public void DestroyBubble(){

        if(bubbleSystem != null){
            bubbleSystem.transform.parent = null;
            //bubbleSystem.transform.localScale = new Vector3(particleSize,particleSize,particleSize);
            bubbleSystem.Stop();
            //Destroy(bubbleSystem.gameObject);
        }
    }
    public void Stop()
    {
        StopAllCoroutines();
    }
}
