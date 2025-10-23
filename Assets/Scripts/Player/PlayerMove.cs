using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float horizontalInput;
    [SerializeField] private EnterFishing enterFScript;
    [SerializeField] private bool isFacingRight;
    [SerializeField] private Animator animator;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enterFScript = GetComponent<EnterFishing>();
        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("isFacingRight", isFacingRight);
    }

   
    void Update()
    {
        
        
        if (enterFScript.isFishing)
        {
            horizontalInput = 0;
            isFacingRight = false;
            animator.SetBool("isFacingRight", isFacingRight);
        }else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        Flip();
        
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        
    }
    private void FixedUpdate()
    {
        Move();
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
