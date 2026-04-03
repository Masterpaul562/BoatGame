using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    [SerializeField] private float y;
    [SerializeField] public Rigidbody2D rb;
    public HarpoonGun gun;
    [SerializeField] private GameObject player;
    private SpriteRenderer render;
    public bool hookedFish = false;
   // public bool submerged;
   
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        render = GetComponent<SpriteRenderer>();
    }
  
    void FixedUpdate()
    {
        
        if ( Vector2.Distance(player.transform.position,this.transform.position)>7)
        {
            gun.StartReel();  
        }
        if (gun.enter.inBoat)
        {
            render.sortingLayerName = "Inside";
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if(other.collider.tag == "Inside")
        {
            gun.StartReel();
        }
    }
}
// floaterScript.enabled = true;
// rb.simulated = false;
// if (!gun.enter.inBoat)
// {
//      gun.isFishing = true;
//    }

