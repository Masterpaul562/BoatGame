using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fishSwimRender;
    private float size;
    public Transform bobber;
    public bool shouldSwimToBobber;
    public float randomY;
    public int swimDirection;
    public float leftX;
    public float rightX;
    public float speed;
    [SerializeField] public bool shouldFlip = true;
    public bool shouldBeDestroyed;
    private BgScroller scroller;



    private void Awake()
    {
        scroller = GetComponent<BgScroller>();
        shouldBeDestroyed = false;
        size = Random.Range(0.5f, .7f);
        transform.localScale = new Vector2(size, size);
        if (swimDirection == 1)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
        fishSwimRender = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        randomY = Random.Range(-6f, -1f);
    }
    private void Update()
    {
        fishySwim();
    }


    private void fishySwim()
    {
        if (shouldSwimToBobber && bobber.GetComponent<Bobber>().submerged == true)
        {
            if (transform.localScale.x > 0 && swimDirection == 1)
            {
                Flip();
            }
            transform.position = Vector2.MoveTowards(transform.position, bobber.position, Time.deltaTime / speed);
        }
        else
        {
            if (swimDirection == 0)
            {

                if (transform.localScale.x < 0)
                {
                    Flip();
                }

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(rightX, randomY), Time.deltaTime / speed);
                if (Vector2.Distance(transform.position, new Vector2(rightX, randomY)) < 1f)
                {
                    shouldBeDestroyed = true;
                }
            }
            else if (swimDirection == 1)
            {
                if (transform.localScale.x > 0)
                {
                    Flip();

                }

                transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftX, randomY), Time.deltaTime / speed);
                if (Vector2.Distance(transform.position, new Vector2(leftX, randomY)) < 1f)
                {
                    shouldBeDestroyed = true;
                }
            }

        }

    }
    public void Flip()
    {

        if (shouldFlip)
        {
            shouldFlip = false;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        }
    }


}
