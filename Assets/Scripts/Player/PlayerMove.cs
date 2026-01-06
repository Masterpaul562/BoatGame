using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] public float horizontalInput;
    [SerializeField] private HarpoonGun enterFScript;
    public bool isFacingRight;
    [SerializeField] private Animator animator;
    public bool freeze;
    public bool isMoving;
    private float currentVel;
    [SerializeField] private float maxSpeed;
    public bool canFlip;


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
        if (canFlip)
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
            moveSpeed = 3;
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


        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));

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
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
            currentVel = rb.velocity.x;
            animator.SetBool("isMoving", true);
        }
        else if (!isMoving && !this.GetComponent<EnterBoat>().inBoat)
        {

            animator.SetBool("isMoving", false);
            currentVel = Mathf.MoveTowards(currentVel, 0, Time.deltaTime * 10);
            rb.velocity = new Vector2(currentVel, rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
        }
    } 
    private void FlipCheck()
    {
        if (isFacingRight && horizontalInput < 0f || !isFacingRight && horizontalInput > 0f)
        {
            canFlip = false;
            Debug.Log("Turned");
            animator.SetTrigger("Turn");
            isFacingRight = !isFacingRight;            
            animator.SetBool("isFacingRight", isFacingRight);
        }


    }
    public void Flip()
    {
        canFlip = true;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }
}
