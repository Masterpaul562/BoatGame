using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
    float y;

    public WaveDeformer wave;
    public FishEngineReal speed;

   
    public float yOffset;
    public float speedMult;
    public float defaultSpeed;
   // public float rotationOffset;
   // public float rotationPower;


    private void Update()
    {
        float waveValue = Mathf.Sin(transform.position.x * wave.frequency + wave.waveTime);
        y = waveValue * wave.amplitude;

        // Move boat vertically
        transform.position = new Vector2(transform.position.x, y + yOffset);



        float finalSpeed =  defaultSpeed+(speed.knots * speedMult);
        transform.Translate(Vector2.left * finalSpeed * Time.deltaTime);

        // Rotate boat based on wave tilt
        //   Quaternion rot = Quaternion.Euler(0, 0, y * rotationPower + rotationOffset);
        //  transform.rotation = Quaternion.Slerp(transform.rotation, rot, Time.deltaTime * 2);
    }
}
