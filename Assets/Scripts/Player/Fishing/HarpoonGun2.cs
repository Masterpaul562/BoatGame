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
    [SerializeField] private Transform harpEnd;
    private Animator anim;

    [Header("Settings")]
    [SerializeField] private int power;
    public float rotationSpeed;
    public KeyCode inputKey;
    public KeyCode fireKey;
    private bool canCast = true;
    private bool canFire = false;
    private Quaternion ogRotation;
    private float distance;

    [Header("Info")]
    public bool isFishing;
    public bool hasFire;

    private void Start()
    {
        harpoon = transform.GetChild(0).gameObject;
        anim = harpoon.GetComponent<Animator>();
        ogRotation = harpoon.transform.rotation;
    }

    private void Update()
    {
        
        if(canCast && Input.GetKeyDown(inputKey))
        {
            PrepHarpoon();
        }else if (Input.GetKeyUp(inputKey))
        {
            anim.SetTrigger("Stow");
        }

        if(canFire && Input.GetKeyDown(fireKey))
        {
            anim.SetTrigger("Fire");
        }
        anim.SetFloat("Speed", player.GetComponent<PlayerMove>().animator.GetFloat("Speed"));

        ReelCheck();
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
        harpoon.transform.rotation = ogRotation;
    }

    public void Fire()
    {
        line.SetActive(true);
        harpHead.GetComponent<Rigidbody2D>().simulated = true;
        hasFire = true;

        if (player.GetComponent<Animator>().GetBool("isFacingRight"))
        {
            harpHead.GetComponent<Rigidbody2D>().AddForce(harpoon.transform.right * power, ForceMode2D.Impulse);
        }
        else
        {
            harpHead.GetComponent<Rigidbody2D>().AddForce((harpoon.transform.right * power) * -1, ForceMode2D.Impulse);
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
