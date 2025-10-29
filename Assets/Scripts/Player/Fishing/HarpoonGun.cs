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
            Debug.Log("yay");
            StartCoroutine(Harpoon());
        }
    }

    private IEnumerator Harpoon()
    {
        animator.SetTrigger("PullHarpoonOut");
        freezePlayer.freeze = true;
        freezePlayer.horizontalInput = 0;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        yield return new WaitForSeconds(1f);
        
        while (Input.GetKey(key)&& horz == 0) 
        {
            Debug.Log("yayaya");
            //Charge thing
            yield return null;
        }
        if(Input.GetKeyUp(key))
        {
            yield return new WaitForSeconds(0.5f);

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
}
