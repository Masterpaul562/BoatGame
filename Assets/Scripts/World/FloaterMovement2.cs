using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloaterMovement2 : MonoBehaviour
{
    public float amp;
    public float speed = 1.0f;
    private float lastY;
    
    void Start()
    {
        
    }

   
    void Update()
    {
        float y = amp*Mathf.Sin(Time.time*speed);
        transform.position = Vector2.MoveTowards(transform.position, new Vector2 (transform.position.x, y),Time.deltaTime*2);
        Quaternion rot = Quaternion.Euler(0, 0, y*10);
        transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime*2);
        lastY = y;
    }
}
