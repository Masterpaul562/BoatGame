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
    private Vector2 ogSize;
    private Animator animator;
    

    [Header("Settings")]
    public int leaveWaitMin;
    public int leaveWaitMax;  

    public int emoteWaitMin;
    public int emoteWaitMax;

    public float speedMultiplier;
    public int glideHeight;

    public float bigFlySize;
    public float sizeChangeSpeed;
    public float perchSpeed;
    

    private bool shouldGlide;
    private bool shouldDespawn = true;
    private bool perching = false;
    

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
        StartCoroutine(NaturalLeave());
        flapAmount = Random.Range(2, 4);
        ogSize = transform.localScale;
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
            if (!perching)
            {
                Flying();
            }
        }
        if (Mathf.Abs(transform.position.x) > cam.GetComponent<CamSizeManager>().worldWidth / 2 || transform.position.y > cam.GetComponent<CamSizeManager>().worldHeight - cam.transform.position.y)
        {
            if (shouldDespawn)
            {
                shouldDespawn = false;
                StartCoroutine(Despawn());
            }
        }
    }


    private void Flying()
    {
        float speedDiffrence = Mathf.Abs(speed - boat.currentSpeed);
        float camEdge = (cam.GetComponent<CamSizeManager>().worldWidth / 2) + 10;
        float worldHeight = cam.GetComponent<CamSizeManager>().worldHeight - cam.transform.position.y;

        float yOffset = Mathf.Sin(Time.time);
       
        // flying to the right
        if ( speed> boat.currentSpeed)
        {
            if (isGliding) 
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(camEdge, transform.position.y +1), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(camEdge, worldHeight -yOffset), Time.deltaTime * speedDiffrence * speedMultiplier);
              
            }
            ChangeSize(false);
        } else
        {
            // flying to the left
            if (isGliding)
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-camEdge, transform.position.y +1), Time.deltaTime * speedDiffrence * speedMultiplier);
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, new Vector2(-camEdge, worldHeight - yOffset), Time.deltaTime * speedDiffrence * speedMultiplier);
               
            }
            ChangeSize(true);
        }
        if (transform.position.y > glideHeight)
        {
            shouldGlide = true;
        }else { shouldGlide = false; }
      
    }
    


    private IEnumerator Emote()
    {
        while (perched)
        {
            int time = Random.Range(emoteWaitMin, emoteWaitMax);
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

    private IEnumerator NaturalLeave()
    {
        int waitTime = Random.Range(leaveWaitMin, leaveWaitMax);
        yield return new WaitForSeconds(waitTime);
        animator.SetTrigger("TakeOff");
    }

    public void TakeOff()
    {
        transform.parent = null;
        perched = false;
        animator.SetBool("IsFlying", true);
        isFlying = true;
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bobber")
        {
            animator.SetTrigger("TakeOff");
        }
    }

    private void ChangeSize(bool shrink)
    {
        if (shrink)
        {
            if(transform.position.x > boat.transform.position.x)
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale,ogSize, Time.deltaTime * sizeChangeSpeed);
            }
            else
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, Vector2.zero, Time.deltaTime * sizeChangeSpeed);
            }
        }
        else
        {
            if(transform.position.x < boat.transform.position.x)
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, ogSize, Time.deltaTime * sizeChangeSpeed);
            }
            else
            {
                transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(bigFlySize,bigFlySize), Time.deltaTime * sizeChangeSpeed);
            }
        }
    }

    private IEnumerator Despawn()
    {
        Debug.Log("Despawning");
        yield return new WaitForSeconds(5f);
        if(Mathf.Abs(transform.position.x) > cam.GetComponent<CamSizeManager>().worldWidth / 2 || transform.position.y > cam.GetComponent<CamSizeManager>().worldHeight - cam.transform.position.y)
        {
            this.gameObject.SetActive(false);   
            isSpawned = false;
        }
        shouldDespawn = true;
    }

    public IEnumerator Perch()
    {
        shouldDespawn = false;
        perching = true;
        animator.SetTrigger("Soar");
        while(Vector2.Distance(transform.position, perchPosition.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position,perchPosition.position, Time.deltaTime * perchSpeed);
            transform.localScale = transform.localScale = Vector2.MoveTowards(transform.localScale, ogSize, Time.deltaTime * sizeChangeSpeed);
            yield return null;
        }

        transform.position = perchPosition.position;

        animator.SetBool("IsFlying", false);
        animator.SetTrigger("Perch");

        isFlying = false;
        perching = false;
        perched = true;
        shouldDespawn = true;

        transform.parent = boat.transform;
        CoughFish();
        yield return null;
    }

    private void CoughFish()
    {
        int random = Random.Range(0, 3);
        Debug.Log(random);
        if(random != 2)
        {
            animator.SetTrigger("Cough");
        }
    }


    //animator functions
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
    private void GiveFish()
    {
        int random = Random.Range(1, 3);
        Debug.Log(random + "YAY");
        player.GetComponent<FishInventory>().AddFishOutside(random);
    }

}
