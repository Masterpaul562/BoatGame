using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float horizontalInput;
    [SerializeField] private HarpoonGun enterFScript;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private Animator animator;
    public bool freeze;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enterFScript = GetComponent<HarpoonGun>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("isFacingRight", isFacingRight);
    }

   
    void Update()
    {
        if(enterFScript.isFishing == false)
        {
        Flip(); 
        }
       
        horizontalInput = Input.GetAxisRaw("Horizontal");
        
        
        
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        
    }
    private void FixedUpdate()
    {
        if (!freeze)
        {
            Move();
        }
        else
        {

        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
    private void Flip()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
            animator.SetBool("isFacingRight", isFacingRight);
        }
        

    }
}
