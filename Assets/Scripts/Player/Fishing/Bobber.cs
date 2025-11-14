using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    [SerializeField] private float y;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Floater floaterScript;
    [SerializeField] private HarpoonGun gun;
    private SpriteRenderer render;
    public bool submerged;
   
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        floaterScript = GetComponent<Floater>();
        render = GetComponent<SpriteRenderer>();
    }
  
    void FixedUpdate()
    {
        
        if ( transform.position.y<y)
        {


            floaterScript.enabled = true;
            rb.simulated = false;
            submerged = true;
            if (!gun.enter.inBoat)
            {
                gun.isFishing = true;
            }
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
           Debug.Log("reel");
        }
    }
}
