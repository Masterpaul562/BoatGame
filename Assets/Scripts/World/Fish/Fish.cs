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
    public Camera cam;
    public bool shouldBeDestroyed;
    private BgScroller scroller;
    public bool bait;
    [SerializeField]private bool flipped;



    private void Awake()
    {
        bait = true;
        scroller = GetComponent<BgScroller>();
        shouldBeDestroyed = false;
        size = Random.Range(0.5f, .7f);
        transform.localScale = new Vector2(size, size);
        fishSwimRender = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        randomY = Random.Range(-6f, -1f);
        fishSwimRender.sortingOrder = Random.Range(0, 9);
        flipped = true;
    }
    private void Start(){
        if (speed<1.5f)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            flipped = false;
        }
    }
    private void Update()
    {
        fishySwim();
        if (Mathf.Abs(transform.position.x - bobber.position.x) < 4 && bait && bobber.gameObject.GetComponent<Bobber>().submerged)
        {
            if(bobber.position.x<transform.position.x&&!flipped||bobber.position.x>transform.position.x+3f&&flipped)
            {
                Debug.Log("Baited");
                Baited();
            }
        }
    }


    private void fishySwim()
    {
        if (shouldSwimToBobber && bobber.GetComponent<Bobber>().submerged == true)
        {
          
            transform.position = Vector2.MoveTowards(transform.position, bobber.position, Time.deltaTime / speed);
        }
        else
        {               
                leftX = cam.transform.position.x - cam.GetComponent<CamSizeManager>().worldWidth / 2;
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftX-6, randomY), Time.deltaTime / speed);
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
    private void Baited()
    {
        bait = false;
        int dist = Mathf.RoundToInt(Vector2.Distance(transform.position, bobber.position));
        int random = Random.Range(0, dist);
        Debug.Log(random + "  ");
        if (random == 0)
        {
            shouldSwimToBobber = true;
        }
    }
    public bool DestroyCheck()
    {
        Vector3 point = cam.WorldToViewportPoint(transform.position);           
        if (point.x < -0.1f)
        {
            return true;
        }
        else { return false; }
    }   
}
