using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{

    [SerializeField] private float amplitude, frequency, verticalOffset;
    public float[] xValue;
    public float[] xValueIncrease;
    [SerializeField] private int speed;

    private void Awake()
    {
        xValue = new float[11];
        xValueIncrease = new float[11];
    }
   
    public float WavePos(float vertOffset, int i)
    {

        verticalOffset = vertOffset;
        float position = amplitude * Mathf.Sin(xValue[i] / frequency) + verticalOffset;
        float step = position - transform.position.y;
        transform.Translate(new Vector2(speed * Time.deltaTime, step));
        xValue[i] += xValueIncrease[i];
        return transform.position.y;
    }

}
