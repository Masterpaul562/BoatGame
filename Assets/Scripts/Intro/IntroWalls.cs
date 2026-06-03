using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWalls : MonoBehaviour
{
    public int maxHitAmount;
    public Quaternion rotation;
    public float displaceAmount;

    private int hitAmount;
    private bool started = false;
    private bool shouldRumble;
    private Vector3 startPosition;
    private Vector3 lastPosition;
    


    private void Start()
    {
        startPosition = transform.position; 
        lastPosition = transform.position;
    }


    private void Update()
    {
        if (maxHitAmount <= hitAmount && !started)
        {
            started = true;
            StartCoroutine(DestroyWall());
        }
    }


    private IEnumerator DestroyWall()
    {
        //StartCoroutine(Rumble());
        while (transform.rotation.z != rotation.z)
        {
           // transform.Translate(Vector2.down);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime);
          
            yield return null ;
        }
        while (Vector3.Distance(startPosition, transform.position) < 20)
        {
            transform.position = lastPosition;
            Vector2 direction = new Vector2(transform.position.x, transform.position.y - 1);
            transform.position = Vector2.MoveTowards(transform.position, direction, Time.deltaTime * 2);
            lastPosition = transform.position;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime);
            yield return null ;
        }
        Destroy(this.gameObject);
    }

    private IEnumerator Rumble()
    {
        shouldRumble = true;
        while (shouldRumble)
        {
            float randomX = Random.Range(-displaceAmount, displaceAmount);
            float randomY = Random.Range(-displaceAmount, displaceAmount);


            Vector3 randomPosition = new Vector3(lastPosition.x +randomX, lastPosition.y + randomY, transform.position.z);
            transform.position = randomPosition;
            yield return new WaitForSeconds(0.05f) ;
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
