using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fishSwimRender;
    public Transform bobber;
   public bool shouldSwimToBobber;
   public float randomY;   
    public int swimDirection;
    public float leftX;
    public float rightX;
    public float speed;
        


    private void Awake()
    {
       fishSwimRender = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        randomY = Random.Range(-6f, -1f);
    }
    private void Update()
    {
        fishySwim();
    }
    

    private void fishySwim() {
        if (shouldSwimToBobber&& bobber.GetComponent<Bobber>().submerged == true )
        {
            transform.position = Vector2.MoveTowards(transform.position, bobber.position, Time.deltaTime / speed);
        }
        else
        {
            if (swimDirection == 0)
            {
                //FLIPTHE FISH
                //fishSwimRender.flipX = false;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightX, randomY), Time.deltaTime / speed);
                if (Vector2.Distance(transform.position, new Vector2(rightX, randomY)) < 1f)
                {
                    swimDirection = 1;
                    randomY = Random.Range(-8f, -1f);
                }
            }
            else
            {
                fishSwimRender.flipX = true;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftX, randomY), Time.deltaTime / speed);
                if (Vector2.Distance(transform.position, new Vector2(leftX, randomY)) < 1f)
                {
                    swimDirection = 0;
                    randomY = Random.Range(-8f, -1f);
                }
            }

        }
    
    }


}
