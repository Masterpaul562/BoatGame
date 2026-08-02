 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [Header("Refrence Objects")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private HarpoonGun2 harpoon;
    [SerializeField] private GameObject harpHead;
    public Animator animator;

    [Header("Info")]
    public bool isFacingRight;
    public bool freeze;
    public bool isMoving;
    public bool isTurning;
    public float currentVel;
    public float horizontalInput;

    [Header("Settings")]
    public float maxSpeed;
    public float insideSpeed;
    public float moveSpeed;
    public bool canFlip;
    



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        animator.SetFloat("Speed", 0);
        animator.SetBool("isFacingRight", isFacingRight);
    }


    void Update()
    {

        if (canFlip&& !freeze)
        {
            FlipCheck();
        }

        if (!freeze)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }else
        {
            horizontalInput = 0;
        }
        
        if (this.GetComponent<EnterBoat>().inBoat)
        {
            moveSpeed = insideSpeed;
        }

        if (horizontalInput != 0)
        {
            isMoving = true;
            if (!this.GetComponent<EnterBoat>().inBoat)
            {
                moveSpeed = Mathf.MoveTowards(moveSpeed, maxSpeed, Time.deltaTime * 5);
            }
        }
        else
        {
            isMoving = false;
            if (!this.GetComponent<EnterBoat>().inBoat)
            {
                moveSpeed = Mathf.MoveTowards(moveSpeed, 1, Time.deltaTime * 5);
            }
        }


        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x));

    }
    private void FixedUpdate()
    {
        if (!freeze)
        {
            Move();
        }

    }

    private void Move()
    {
        if (isMoving)
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
            currentVel = rb.linearVelocity.x;
            animator.SetBool("isMoving", true);
        }
        else if (!isMoving && !this.GetComponent<EnterBoat>().inBoat)
        {

            animator.SetBool("isMoving", false);
            currentVel = Mathf.MoveTowards(currentVel, 0, Time.deltaTime * 10);
            rb.linearVelocity = new Vector2(currentVel, rb.linearVelocity.y);
        }
        else
        {
            rb.linearVelocity = new Vector2(horizontalInput * moveSpeed, rb.linearVelocity.y);
        }
    } 


    private void FlipCheck()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            canFlip = false;
            isTurning = true;

            animator.SetTrigger("Turn");
            harpoon.Turn();
           // harpHead.GetComponent<Rigidbody2D>().rotation = -harpHead.GetComponent<Rigidbody2D>().rotation;

            isFacingRight = !isFacingRight;            
            animator.SetBool("isFacingRight", isFacingRight);
            GetComponent<EnterBoat>().canEnter = false;
        }


    }



    public void Flip()
    {
        //harpHead.GetComponent<Rigidbody2D>().rotation = -harpHead.GetComponent<Rigidbody2D>().rotation;
        GetComponent<EnterBoat>().canEnter = true;
        canFlip = true;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
        isTurning = false;
        
    }


    public void Recoil()
    {
        if (isFacingRight)
        {
            transform.Translate(Vector2.left * 0.05f);
        } else
        {
            transform.Translate(Vector2.right * 0.05f);
        }
       
    }


    public void Unfreeze()
    {
        freeze = false;
    }
}
