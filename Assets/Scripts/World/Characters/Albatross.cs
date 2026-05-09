using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Albatross : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject player;
    [SerializeField] private Camera cam;
    [SerializeField] private SpeedManager boat;
    [SerializeField] private Transform perchPosition;
    private Animator animator;

    [Header("Settings")]
    public float waitMin;
    public float waitMax;
    public float speedMultiplier;
    public int glideHeight;
    private bool shouldGlide;
    

    [Header("Info")]
    public bool perched;
    public bool isFlying;
    public bool isGliding;
    public bool isSpawned;
    public float speed;
    public int flaps;
    public int flapAmount;



    private void Start()
    {
        animator = GetComponent<Animator>();
        perched = true;
        StartCoroutine(Emote());
        flapAmount = Random.Range(2, 4);
    }

    private void Update()
    {
        if(!isSpawned)
        {
            GetComponent<SpriteRenderer>().enabled = false;
        }
        else
        {
            GetComponent<SpriteRenderer>().enabled = true;
        }

        if ( isFlying )
        {
            if (shouldGlide)
            {
                if (flaps > flapAmount)
                {
                    flaps = 0;
                    flapAmount = flapAmount = Random.Range(2, 4);
                    animator.SetTrigger("Glide");
                    // isGliding = true;
                }
            }
            Flying();
        }
    }


    private void Flying()
    {
        float speedDiffrence = Mathf.Abs(speed - boat.currentSpeed);
        float camEdge = (cam.GetComponent<CamSizeManager>().worldWidth / 2) + 10;
        float worldHeight = cam.GetComponent<CamSizeManager>().worldHeight - cam.transform.position.y;

        float yOffset = Mathf.Sin(Time.time);
       
        if ( speed> boat.currentSpeed)
        {
            if (isGliding) 
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(camEdge, transform.position.y -1), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(camEdge, worldHeight -yOffset), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
        } else
        {
            if (isGliding)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-camEdge, transform.position.y - 1), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-camEdge, worldHeight - yOffset), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
           
        }
        if (transform.position.y > glideHeight)
        {
            shouldGlide = true;
        }else { shouldGlide = false; }
       // transform.position = new Vector2(transform.position.x,transform.position.y + y);
    }
    
    private void Spawn()
    {

    }

    private IEnumerator Emote()
    {
        while (perched)
        {
            float time = Random.Range(waitMin, waitMax);
            yield return new WaitForSeconds(time);

            int emote = Random.Range(0, 2);
            
            if (emote == 0 )
            {
                animator.SetTrigger("Peck");
            }else if (emote == 1)
            {
                animator.SetTrigger("Ruffle");
            }
        }
    }

    public void TakeOff()
    {
        transform.parent = null;
        perched = false;
        animator.SetBool("IsFlying", true);
        isFlying = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bobber")
        {
            animator.SetTrigger("TakeOff");
        }
    }
    private void Flap()
    {
        if (shouldGlide)
        {
            flaps++;
        }
    }
    private void Gliding()
    {
        isGliding = true;
    }
    private void NotGliding()
    {
        isGliding = false;
    }

}
