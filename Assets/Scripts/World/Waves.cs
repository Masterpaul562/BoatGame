using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    [SerializeField] private float amplitude, frequency, verticalOffset, xValue;
   
     private float xValueIncrease = 0.3f;
    [SerializeField] private int speed;


    private void FixedUpdate()
    {
        // x value just needs to increase
        float position = amplitude * Mathf.Sin(xValue/frequency)+verticalOffset;
        
        float step = position - transform.position.y;
        transform.Translate(new Vector2(speed * Time.deltaTime,step));
        xValue += xValueIncrease;
    }
    public float WavePos(float vertOffset)
    {
        verticalOffset = vertOffset;
        float position = amplitude * Mathf.Sin(xValue / frequency) + verticalOffset;
        float step = position - transform.position.y;
        transform.Translate(new Vector2(speed * Time.deltaTime, step));
        xValue += xValueIncrease;
        return transform.position.y;


    }

}
