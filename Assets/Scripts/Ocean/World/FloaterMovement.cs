using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterMovement : MonoBehaviour
{
    [SerializeField] public Camera cam;
    public float moveAmount;

    void FixedUpdate()
    {
       
            transform.position = new Vector2(transform.position.x- moveAmount,transform.position.y);
        
    }
    public bool DestroyThis()
    {
        Vector3 point = cam.WorldToViewportPoint(transform.position);
        if (point.x < -0.2f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
