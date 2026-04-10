using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCity : MonoBehaviour
{
    [Range(-1, 1)] public int direction;
    public float speed;

    private float x;


    private void Update()
    {
        MoveCity();
    }

    private void MoveCity()
    {

        transform.position = Vector2.MoveTowards(transform.position, new Vector2(transform.position.x -1,transform.position.y), Time.deltaTime * direction * speed) ;
    }
}
