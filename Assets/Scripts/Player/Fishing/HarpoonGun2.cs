using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun2 : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private GameObject line;
    [SerializeField] private GameObject harpHead;
    [SerializeField] private Transform headHolder;
    [SerializeField] private Transform harpEnd;
    private Animator anim;

    [Header("Settings")]
    [SerializeField] private int power;
    public float rotSpeed;
    public KeyCode inputKey;
    public KeyCode fireKey;
    public int lookAngle;
    private bool canCast = true;
    private bool canFire = false;
    private Quaternion quatZero;
    private Quaternion quatUp;
    private Quaternion quatDown;
    private float distance;                                                           


    [Header("Info")]
    public bool isFishing;
    public bool hasFire;


    private void Start()
    {
        harpoon = transform.GetChild(0).gameObject;
        anim = harpoon.GetComponent<Animator>();
        quatZero = Quaternion.Euler(0, 0, 0);   
        
    }

    private void Update()
    {
      
        anim.SetFloat("Speed", player.GetComponent<PlayerMove>().animator.GetFloat("Speed"));
        Rotate();
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
        canFire= false;
        line.SetActive(false);
        harpoon.SetActive(false);
        
    }

    public void Fire()
    {
        line.SetActive(true);
        harpHead.GetComponent<Rigidbody2D>().simulated = true;
        hasFire = true;
        canFire = false;
        harpHead.transform.parent = null;

        if (player.GetComponent<Animator>().GetBool("isFacingRight"))
        {
            harpHead.GetComponent<Rigidbody2D>().AddForce(harpoon.transform.right * power, ForceMode2D.Impulse);
            harpHead.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.GetComponent<Rigidbody2D>().velocity.x,0), ForceMode2D.Impulse);
        }
        else
        {
            harpHead.GetComponent<Rigidbody2D>().AddForce((-harpoon.transform.right * power), ForceMode2D.Impulse);
            harpHead.GetComponent<Rigidbody2D>().AddForce(new Vector2(player.GetComponent<Rigidbody2D>().velocity.x, 0), ForceMode2D.Impulse);
        }
    }

    public IEnumerator Reel()
    {
        harpHead.GetComponent<Rigidbody2D>().simulated = false;     

        distance = 100f;
        while (distance > 0.1f)
        {
            Vector2 pos = Vector2.MoveTowards(harpHead.transform.position, harpEnd.position, Time.deltaTime * 20);
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
        yield return null;
    }
    
    private void ReelCheck()
    {
        if (Vector2.Distance(harpEnd.position, harpHead.transform.position) > 7)
        {
            StopAllCoroutines();
            StartCoroutine(Reel());
        }
    }

    private void Rotate()
    {
        float vert = Input.GetAxisRaw("Vertical");
        Quaternion quatUp = Quaternion.Euler(0, 0, lookAngle * -player.transform.localScale.x / -(Mathf.Abs(player.transform.localScale.x)));
        Quaternion quatDown = Quaternion.Euler(0, 0, -lookAngle * -player.transform.localScale.x/-(Mathf.Abs(player.transform.localScale.x)));
        if (vert > 0)
        {           
           harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation, quatUp, Time.deltaTime * rotSpeed);
            
        }
        if (vert < 0)
        {
            harpoon.transform.rotation = Quaternion.Slerp(harpoon.transform.rotation, quatDown, Time.deltaTime * rotSpeed);
        }
    }

    private void SetInside()
    {
        var inside = player.GetComponent<EnterBoat>();
        if (inside.inBoat)
        {
           // line.GetComponent<LineRenderer>().sortingLayerName = "Inside";
            //line.GetComponent<LineRenderer>().sortingOrder = 1;
        }
        else if (!inside.inBoat)
        {
           // line.GetComponent<LineRenderer>().sortingLayerName = "Default";
           // line.GetComponent<LineRenderer>().sortingOrder = 0;
        }
    }

    public void Turn()
    {
        anim.SetTrigger("Turn");
    }

}
