using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroWalls : MonoBehaviour
{
    public GameObject sparks;
    public GameObject dust;
    public int maxHitAmount;
    public float maxRotation;
    public float rotation;
    public float displaceAmount;
    public float rotationSpeed;
    public bool moveAway;
    

    [SerializeField] private int hitAmount;
  //  private bool started = false;
    private bool shouldRumble;
    private Vector3 startPosition;
    private Vector3 lastPosition;
   
    


    private void Start()
    {
        startPosition = transform.position; 
        lastPosition = transform.position;
        rotation = 0;
    }


    private void Update()
    {
        if (moveAway)
        {
            Move();
        }
       
    }


    private void DestroyWall()
    {
        rotation += maxRotation / 3;
        transform.rotation = Quaternion.Euler(0, 0, rotation);
        if(rotation == maxRotation )
        {
            moveAway = true;
        }
      //  while (Vector3.Distance(startPosition, transform.position) < 20)
       // {
       //     transform.position = lastPosition;
       //     Vector2 direction = new Vector2(transform.position.x, transform.position.y - 1);
        //    transform.position = Vector2.MoveTowards(transform.position, direction, Time.deltaTime * 2);
        ///    lastPosition = transform.position;
            
        //    yield return null ;
      //  }
        
    }


    private void Move()
    {
        rotationSpeed += 2;
        Quaternion flat = Quaternion.Euler(0, 0, 270);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, flat,Time.deltaTime*rotationSpeed);
        GetComponent< BoxCollider2D >().enabled = false;
        if(transform.rotation == flat)
        {
            dust.SetActive(true);
            Destroy(this);
        }
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
            Instantiate(sparks, other.contacts[0].point,sparks.transform.rotation);
            DestroyWall();
        }
    }

}
