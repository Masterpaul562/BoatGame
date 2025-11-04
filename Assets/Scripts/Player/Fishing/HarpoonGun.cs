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
            if( !isFishing)
            {
            freezePlayer.freeze = true;
            StartCoroutine(Harpoon());
            }else 
            {
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
                animator.SetBool("StowHarpoon", false);
                StopCoroutine(Harpoon());
            }
        }
    }

    private IEnumerator Harpoon()
    {
        noFire = true;
        bool shouldStow = true;
        animator.SetTrigger("PullHarpoonOut");
        
        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(1f);
         
        while (Input.GetKey(key)&& horz == 0) 
        {
            Debug.Log("yayaya");
            shouldStow = false;
            shouldFire = true;
            if (harpoonPower < 8)
            {
                harpoonPower++;
            }
            yield return new WaitForSeconds(.5f);;
        }
        yield return new WaitForSeconds(.2f);
        if (horz != 0&&noFire||shouldStow)
        {
            shouldFire=false;
            animator.SetTrigger("StowHarpoon");



            yield return new WaitForSeconds(1f);
            Debug.Log("UnFreeze");
            freezePlayer.freeze = false;
            yield return null;
        }
    }
    private void Fire()
    {
        bobber.SetActive(true);
       bobber.GetComponent<Rigidbody2D>().simulated = true;
        isFishing = true;
        line.gameObject.SetActive(true);
        if (animator.GetBool("isFacingRight"))
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(harpoonPower * 2, harpoonPower * 0.5f), ForceMode2D.Impulse);
        }else
        {
            bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(harpoonPower * -2, harpoonPower * -0.5f), ForceMode2D.Impulse);
        }
       harpoonPower = 1;
    }

    private IEnumerator ReelIn()
    {
        Debug.Log("didit");
        while(distance>1)
        {       
            Debug.Log("yay");
          Vector2 pos = Vector2.MoveTowards(bobber.transform.position, this.transform.position, Time.deltaTime * 10);
            bobber.transform.position = pos;
            distance = Vector2.Distance(transform.position, bobber.transform.position);
            yield return null;
        }                      
         bobber.SetActive(false);
         line.gameObject.SetActive(false);
         bobber.GetComponent<Bobber>().rb.simulated = true;
         bobber.GetComponent<Bobber>().submerged = false;      
         yield return null;         
            
    }

}
