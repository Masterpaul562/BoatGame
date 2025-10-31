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

    private void Start()
    {
        animator = GetComponent<Animator>();
        freezePlayer = GetComponent<PlayerMove>();
    }


    private void Update()
    {
        horz = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(key))
        {
            
            freezePlayer.freeze = true;
            StartCoroutine(Harpoon());
        }
    }

    private IEnumerator Harpoon()
    {
        animator.SetTrigger("PullHarpoonOut");
        
        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(1f);
         
        while (Input.GetKey(key)&& horz == 0) 
        {
            Debug.Log("yayaya");
         
           // if(harpoonPower <7)
           // {
            harpoonPower++;
            
          //  }
            
            yield return new WaitForSeconds(1f);;
        }
        if(Input.GetKeyUp(key))
        {
            yield return new WaitForSeconds(0.3f);
            Fire();
            animator.SetTrigger("Fire");
        }
        else
        {
            animator.SetTrigger("StowHarpoon");
           
        }
        
        yield return new WaitForSeconds(1f);
        Debug.Log("UnFreeze");
        freezePlayer.freeze = false;
        yield return null;
    }
    private void Fire()
    {
        bobber.SetActive(true);
       bobber.GetComponent<Rigidbody2D>().simulated = true;
       bobber.GetComponent<Rigidbody2D>().AddForce(new Vector2(harpoonPower*2,harpoonPower*0.5f),ForceMode2D.Impulse);
       harpoonPower = 0;
    }

}
