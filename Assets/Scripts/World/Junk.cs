using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Junk : MonoBehaviour
{
    private FloaterMovement movement;
    private float movementOffset;

    private void Awake()
    {
        float size = Random.Range(0.7f, 1.5f);
        transform.localScale = new Vector2(size, size);
        float speed = Random.Range(0.02f, 0.035f);
        movement = GetComponent<FloaterMovement>();
        movement.moveAmount = speed;



    }
}
