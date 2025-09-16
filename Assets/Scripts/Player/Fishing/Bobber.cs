using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{

    [SerializeField] private float y;
    [SerializeField] public Rigidbody2D rb;
    [SerializeField] private Floater floaterScript;
   
   

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        floaterScript = GetComponent<Floater>();
    }
    private void Update()
    {
       
    }
    void FixedUpdate()
    {
        
        if ( transform.position.y<y)
        {


            floaterScript.enabled = true;
            rb.simulated = false;
            //Destroy(rb);
            
            // rb.constraints = RigidbodyConstraints2D.FreezePositionX| RigidbodyConstraints2D.FreezePositionY;
            // rb.bodyType = RigidbodyType2D.Kinematic;
            //  rb.velocity = Vector2.zero;
            //   
        }
    }
}
