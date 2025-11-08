using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun : MonoBehaviour
{
    public KeyCode key;
    [SerializeField] CameraShake camShake;
    private Animator animator;
    private float horz;
    private PlayerMove freezePlayer;
    [SerializeField] private GameObject bobber;
    [SerializeField]private float harpoonPower;
    [SerializeField] private LineRenderController line;
    private bool  shouldFire;
    private bool noFire;
    public bool isFishing;
    [SerializeField]private float distance = 100;
    [SerializeField]public Transform harpoonEnd;
    [SerializeField]private FishManager fish;
   [SerializeField] private bool canCast;
    [SerializeField] private bool canReel = true;


    private void Start()
    {
        animator = GetComponent<Animator>();
        freezePlayer = GetComponent<PlayerMove>();
      
    }


    private void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(key) )
        {            
            if( !isFishing && canCast)
            {
                canCast = false;
            freezePlayer.freeze = true;
            StartCoroutine(Harpoon());
            }else if (isFishing && !canCast&& canReel) 
            {
                canReel = false;
                StartCoroutine(ReelIn());
            }
        }
        if(Input.GetKeyUp(key))
        {
            if (shouldFire)
            {
                noFire = false;
                shouldFire = false; 
                Fire();
                animator.SetTrigger("Fire");
                harpoonEnd.position = new Vector2(harpoonEnd.transform.position.x-0.1f,line.transform.position.y);
                animator.SetBool("StowHarpoon", false);
                StopCoroutine(Harpoon());
            }
        }
    }

    private IEnumerator Harpoon()
    {
        noFire = true;
        bool shouldStow = true;
        shouldFire = false;
        animator.SetTrigger("PullHarpoonOut");
        
        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(1f);
         
        while (Input.GetKey(key)&& horz == 0) 
        {
            Debug.Log("Charching");
            shouldStow = false;
           // noFire = false;
            shouldFire = true;
            if (harpoonPower < 8)
            {
                harpoonPower++;
            }
            yield return new WaitForSeconds(.3f);;
        }
        canCast = false;
        yield return new WaitForSeconds(.2f);
        if (horz != 0&&noFire||shouldStow)
        {
            shouldFire=false;
            animator.SetTrigger("StowHarpoon");
            yield return new WaitForSeconds(1f);
            Debug.Log("UnFreeze");
            freezePlayer.freeze = false;
            canCast = true;
            yield return null;
            
        }
    }
    private void Fire()
    {
        bobber.SetActive(true);
        
        canCast= false;
        Debug.Log("Fired");
        line.gameObject.SetActive(true);
        bobber.GetComponent<Floater>().enabled = false;
        if (animator.GetBool("isFacingRight"))
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(harpoonPower * 2.5f, 1), ForceMode2D.Impulse);
        }else
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(harpoonPower * -2.5f,1), ForceMode2D.Impulse);
        }
        bobber.GetComponent<Rigidbody2D>().simulated = true;
        
       harpoonPower = 1;
        
    }

    private IEnumerator ReelIn()
    {
        Debug.Log("StartReelIn");
      
        bobber.GetComponent<Rigidbody2D>().simulated = false;
        distance = 100;
        fish.HookFish();
        while(distance>0.1f)
        {       
            
          Vector2 pos = Vector2.MoveTowards(bobber.transform.position, harpoonEnd.position, Time.deltaTime * 10);
            bobber.transform.position = pos;
            distance = Vector2.Distance(harpoonEnd.position, bobber.transform.position);
            yield return null;
        }                      
         bobber.SetActive(false);
         line.gameObject.SetActive(false);
         bobber.GetComponent<Bobber>().rb.simulated = true;
         bobber.GetComponent<Bobber>().submerged = false;      
         isFishing = false;
         freezePlayer.freeze = false;
         shouldFire=false;
        if (fish.canHook)
        {
            Debug.Log("catch");
            fish.SecureFish();
        }
          animator.SetTrigger("StowHarpoon");
        yield return new WaitForSeconds(1f);
        Debug.Log("CAnCast");
         canCast = true;
        canReel = true;
        yield return null;         
            
    }

}
