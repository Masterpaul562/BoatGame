using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parrallax : MonoBehaviour
{
    public float horzInput;
    public float moveSpeed;

    // Update is called once per frame
    void Update()
    {
        horzInput = Input.GetAxisRaw("Horizontal");
        Vector3 position = transform.position;
        position.x = position.x - (horzInput * moveSpeed);
        transform.position = position;
        
    }
}
