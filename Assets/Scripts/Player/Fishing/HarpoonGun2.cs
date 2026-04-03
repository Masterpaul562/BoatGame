using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarpoonGun2 : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject harpoon;
    [SerializeField] private GameObject bobber;
    [SerializeField] private GameObject line;
    private Animator anim;

    [Header("Settings")]
    public float rotationSpeed;
    public KeyCode inputKey;
    public KeyCode fireKey;
    private bool canCast = true;
    private Quaternion ogRotation;

    [Header("Info")]
    [SerializeField] private int powerLevel;
    public bool isFishing;

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

        anim.SetFloat("Speed", player.GetComponent<PlayerMove>().animator.GetFloat("Speed"));
    }

    private void PrepHarpoon()
    {
        isFishing = true;
        canCast = false;
        harpoon.SetActive(true);
        anim.SetTrigger("Prep");
    }

    public void StowHarpoon()
    {
        isFishing = false;  
        canCast = true;
        harpoon.SetActive(false);
        harpoon.transform.rotation = ogRotation;
    }

    private void Fire()
    {

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
