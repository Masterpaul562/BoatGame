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

    [Header("Info")]
    public bool perched;
    public bool isFlying;
    public bool isSpawned;
    public float speed;



    private void Start()
    {
        animator = GetComponent<Animator>();
        perched = true;
        StartCoroutine(Emote());
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
            Flying();
        }
    }


    private void Flying()
    {
        float speedDiffrence = Mathf.Abs(speed - boat.currentSpeed);
        float camEdge = (cam.GetComponent<CamSizeManager>().worldWidth / 2) + 1;

        if ( speed> boat.currentSpeed)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(camEdge,boat.transform.position.y+3 ), Time.deltaTime * speedDiffrence * speedMultiplier);
        } else
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(-camEdge, boat.transform.position.y + 1), Time.deltaTime * speedDiffrence * speedMultiplier);
        }
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

}
