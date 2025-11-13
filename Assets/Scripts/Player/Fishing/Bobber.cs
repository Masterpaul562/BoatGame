using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    [SerializeField] private float y;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Floater floaterScript;
    [SerializeField] private HarpoonGun gun;
    public bool submerged;
   
   

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        floaterScript = GetComponent<Floater>();
    }
  
    void FixedUpdate()
    {
        
        if ( transform.position.y<y)
        {


            floaterScript.enabled = true;
            rb.simulated = false;
            submerged = true;
            gun.isFishing = true;
            //Destroy(rb);

            // rb.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezePositionY;
            // rb.bodyType = RigidbodyType2D.Kinematic;
            //  rb.velocity = Vector2.zero;
            //   
        }
    }
}
