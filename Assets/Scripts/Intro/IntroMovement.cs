
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroMovement : MonoBehaviour
{

    public float moveSpeed;
    public bool canFlip;
    public bool isMoving;
    public bool freeze;

    public float horizontalInput;
    public bool isFacingRight;
    public float currentVel;

    private Rigidbody2D rb;
    public Animator animator;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("isFacingRight", isFacingRight);
    }
    void Update()
    {
        if (!freeze)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            horizontalInput = 0;
        }

        isMoving = horizontalInput != 0f;

        if (canFlip && !freeze)
        {
            FlipCheck();
        }
        Move();

    }

    private void Move()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
            currentVel = rb.linearVelocity.x;
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
            currentVel = Mathf.MoveTowards(currentVel, 0, Time.deltaTime * 10);
            rb.linearVelocity = new Vector2(currentVel, rb.linearVelocity.y);
        }
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));
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
    public void Recoil()
    {
        if (isFacingRight)
        {
            transform.Translate(Vector2.left * 0.05f);
        }
        else
        {
            transform.Translate(Vector2.right * 0.05f);
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

