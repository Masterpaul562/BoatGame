using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonHead : MonoBehaviour
{
    public HarpoonGun2 harpoon;

    public ParticleSystem blood;

    [SerializeField] private GameObject bubble;

    
    
    
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
            Vector3 bloodPos = new Vector3(other.transform.position.x, other.transform.position.y, blood.transform.position.z); 
            var bloodClone = Instantiate(blood, bloodPos , Quaternion.Euler(0, 0, 0));
            bloodClone.gameObject.SetActive(true);
            //blood.transform.parent = null;
            //blood.Play();
            harpoon.StartReel();
        }
        if(other.gameObject.tag == "Water")
        {
            StartCoroutine(Bubbles());
        }

    }


    private IEnumerator Bubbles()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            Vector3 bubblePos = new Vector3(transform.position.x, transform.position.y, bubble.transform.position.z);
            var newBubble = Instantiate(bubble, bubblePos, Quaternion.Euler(0,0,0));
            // newBubble.GetComponent<
            yield return null;
            //yield return new WaitForSeconds(0.1f);

        }
    }

    public void Stop()
    {
        StopAllCoroutines();
    }
}
