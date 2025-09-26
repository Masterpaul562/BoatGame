using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    public Transform bobber;
   public bool shouldSwimToBobber;
   private float randomY;
    private float randomX;


    private void Awake()
    {
        randomX = Random.Range(-30f, 8f);
        randomY = Random.Range(-8f, -3f);
    }
    private void Update()
    {
        fishySwim();
    }
    

    private void fishySwim() {
        if (shouldSwimToBobber&& bobber.GetComponent<Bobber>().submerged == true )
        {
            transform.position = Vector2.MoveTowards(transform.position, bobber.position, Time.deltaTime / 2);
        }
        else
        {
           
            transform.position = Vector2.MoveTowards(transform.position, new Vector2 (randomX, randomY),Time.deltaTime / 2);
            if(Vector2.Distance(transform.position,new Vector2(randomX,randomY))< 1f){
            randomX = Random.Range(-27f, 7f);
            randomY = Random.Range(-8f, -3f);
            }
        }
    
    }


}
