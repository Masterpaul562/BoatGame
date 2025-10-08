using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterMovement : MonoBehaviour
{
 
    public float moveAmount;

    void Update()
    {
       
            transform.position = new Vector2(transform.position.x- moveAmount,transform.position.y);
        
    }
    public bool DestroyThis()
    {
        if (transform.position.x < -55)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
