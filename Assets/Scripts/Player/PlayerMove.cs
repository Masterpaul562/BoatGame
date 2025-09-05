using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float horizontalInput;
    [SerializeField] private EnterFishing enterFScript;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        enterFScript = GetComponent<EnterFishing>();
    }

   
    void Update()
    {
        
        
        if (enterFScript.isFishing)
        {
            horizontalInput = 0;
        }else
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
        }
    }
    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalInput * moveSpeed, rb.velocity.y);
    }
}
