using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroHarpoon : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private GameObject line;
    [SerializeField] private LineRenderer fishingLine;
    [SerializeField] private Transform headHolder;
    [SerializeField] private Transform direction;
    public GameObject harpHead;
    public Transform harpEnd;
    private Animator anim;
    public Vector2 ogPos;
    public AudioSource source;

    [Header("Settings")]
    [SerializeField] private int power;
    [SerializeField] private float lineLength;
    public int lookAngle;
    public float rotSpeed;
    public KeyCode inputKey;
    public KeyCode fireKey;
    private bool canCast = true;
    private bool canFire = false;
    private Quaternion quatZero;
    private Quaternion quatUp;
    private Quaternion quatDown;
    private float distance;
    private float reelSpeed;


    [Header("Info")]
    public bool isFishing;
    public bool hasFire;
    public bool isReeling;




    private void Start()
    {
        harpoon = transform.GetChild(0).gameObject;
        anim = harpoon.GetComponent<Animator>();
        quatZero = Quaternion.Euler(0, 0, 0);
        ogPos = harpoon.transform.localPosition;
        source = GetComponent<AudioSource>();
    }

    private void Update()
    {

        anim.SetFloat("Speed", player.GetComponent<IntroMovement>().animator.GetFloat("Speed"));

        if (isFishing)
        {
            Rotate();
        }
        InputCheck();
        ReelCheck();
    }


    private void InputCheck()
    {
        if (canCast && Input.GetKeyDown(inputKey))
        {
            PrepHarpoon();
        }
        else if (Input.GetKeyUp(inputKey))
        {
            anim.SetTrigger("Stow");
            harpoon.transform.SetLocalPositionAndRotation(harpoon.transform.localPosition, quatZero);
        }

        if (canFire && Input.GetKeyDown(fireKey))
        {
            anim.SetTrigger("Fire");
            
            player.GetComponent<Animator>().SetTrigger("Fire");
            

            source.Play();
        }
    }

    private void PrepHarpoon()
    {
        isFishing = true;
        canCast = false;
        canFire = true;
        harpoon.SetActive(true);
        anim.SetTrigger("Prep");

    }

    public void StowHarpoon()
    {
        isFishing = false;
        canCast = true;
        canFire = false;
        line.SetActive(false);
        harpoon.transform.SetLocalPositionAndRotation(harpoon.transform.localPosition, quatZero);
        harpoon.SetActive(false);

    }

    public void Fire()
    {
        line.SetActive(true);
        harpHead.GetComponent<Rigidbody2D>().simulated = true;
        hasFire = true;
        canFire = false;
        harpHead.transform.parent = player.transform.parent;

        player.GetComponent<IntroMovement>().Recoil();
        player.GetComponent<IntroMovement>().freeze = true;


        // if (player.GetComponent<Animator>().GetBool("isFacingRight"))
        // {
        Vector2 direction = harpHead.transform.position - this.direction.position;
        harpHead.GetComponent<Rigidbody2D>().AddForce(direction * power, ForceMode2D.Impulse);
        harpHead.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0), ForceMode2D.Impulse);
        // }
        // else
        //{
        //   harpHead.GetComponent<Rigidbody2D>().AddForce((-harpoon.transform.right * power), ForceMode2D.Impulse);
        //   harpHead.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0), ForceMode2D.Impulse);
        // }
    }

    public IEnumerator Reel()
    {
        harpHead.GetComponent<Rigidbody2D>().simulated = false;

        isReeling = true;
       
        distance = 100f;

         reelSpeed = 20;
        
        while (distance > 0.1f)
        {

            Vector2 pos = Vector2.MoveTowards(harpHead.transform.position, harpEnd.position, Time.deltaTime * reelSpeed);
            harpHead.transform.position = pos;
            distance = Vector2.Distance(harpEnd.position, harpHead.transform.position);
            
            yield return null;
        }
        anim.SetTrigger("Reeled");
        line.SetActive(false);
        hasFire = false;
        canFire = true;
        harpHead.transform.parent = headHolder.transform;
        harpHead.transform.SetLocalPositionAndRotation(harpHead.transform.localPosition, quatZero);
        isReeling = false;
        player.GetComponent<IntroMovement>().freeze = false;
        

      
        yield return null;
    }

    

    private void ReelCheck()
    {
        if (Vector2.Distance(harpEnd.position, harpHead.transform.position) > lineLength && !isReeling)
        {
            //StopAllCoroutines();
            StartCoroutine(Reel());
        }
    }

    private void Rotate()
    {
        float vert = Input.GetAxisRaw("Vertical");
        Quaternion quatUp = Quaternion.Euler(0, 0, lookAngle * -player.transform.localScale.x / -(Mathf.Abs(player.transform.localScale.x)));
        Quaternion quatDown = Quaternion.Euler(0, 0, -lookAngle * -player.transform.localScale.x / -(Mathf.Abs(player.transform.localScale.x)));
        if (vert > 0)
        {
            harpoon.transform.rotation = Quaternion.RotateTowards(harpoon.transform.rotation, quatUp, Time.deltaTime * rotSpeed);

        }
        if (vert < 0)
        {
            harpoon.transform.rotation = Quaternion.RotateTowards(harpoon.transform.rotation, quatDown, Time.deltaTime * rotSpeed);
        }
    }

   

    public void Turn()
    {
        anim.SetTrigger("Turn");
    }
    
    public void StartReel()
    {
        StartCoroutine(Reel());
    }
}
