using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovement : MonoBehaviour
{

    public float moveSpeed;
    public bool canFlip;
    public bool isMoving;

    public float horizontalInput;
    public bool isFacingRight;
    public float currentVel;

    private Rigidbody2D rb;
    private Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("isFacingRight", isFacingRight);
    }
    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isMoving = horizontalInput != 0f;

        if(canFlip )
        {
            FlipCheck();
        }
        Move();

    }

    private void Move()
    {
        if (isMoving)
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            currentVel = rb.velocity.x;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            currentVel = Mathf.MoveTowards(currentVel, 0, Time.deltaTime * 10);
            rb.velocity = new Vector2(currentVel, rb.velocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }



    private void FlipCheck()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            canFlip = false;

            animator.SetTrigger("Turn");


            isFacingRight = !isFacingRight;
            animator.SetBool("isFacingRight", isFacingRight);

        }
    }


    private void IntroFlip()
    {
      
        canFlip = true;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;


    }

}
