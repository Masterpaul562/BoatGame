using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fish : MonoBehaviour
{
    [SerializeField] private SpriteRenderer fishSwimRender;
    [SerializeField] private SpriteMask mask;
    private float size;
    public Transform bobber;
    public float randomY;
    public int swimDirection;
    public float leftX;
    public float rightX;
    public float speed;
    [SerializeField] public bool shouldFlip = true;
    public Camera cam;
    public bool shouldBeDestroyed;
    private BgScroller scroller;
    private bool swim = true;
    public bool isHooked = false;
   



    private void Awake()
    {
      
        scroller = GetComponent<BgScroller>();
        shouldBeDestroyed = false;
        size = Random.Range(0.5f, .7f);
        transform.localScale = new Vector2(size, size);
        fishSwimRender = transform.GetChild(0).transform.GetComponent<SpriteRenderer>();
        randomY = Random.Range(-6f, -1f);
        fishSwimRender.sortingOrder = Random.Range(-9, 0);
        
    }
    private void Start()
    {
        if (speed < 1.5f)
        {
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
           
        }
    }
    private void Update()
    {
        mask.sprite = fishSwimRender.sprite;
        if (swim)
        {
            fishySwim();
        }
    }

    private void fishySwim()
    {
        //move fish to bobber
        //  transform.position = Vector2.MoveTowards(transform.position, bobber.position, Time.deltaTime / speed);

        leftX = cam.transform.position.x - cam.GetComponent<CamSizeManager>().worldWidth / 2;
        transform.position = Vector2.MoveTowards(transform.position, new Vector2(leftX - 6, randomY), Time.deltaTime / speed);
    }

    public void Flip()
    {
        if (shouldFlip)
        {
            shouldFlip = false;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ( other.gameObject.tag == "Bobber")
        {
            var script = other.GetComponent<Bobber>();
            if (!script.hookedFish&& !script.gun.isReeling)
            {
                script.hookedFish = true;   
                transform.parent = other.gameObject.transform;
                transform.position = other.transform.position;
                swim = false;
                isHooked = true;
                Debug.Log("yay");
            }
           
        }
    }
}
