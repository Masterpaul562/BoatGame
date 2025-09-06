using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
   [SerializeField] private float waveMax;
   [SerializeField] private float waveMin;
   [SerializeField] private Vector2 wave;
   [SerializeField] private float rotMax;
   [SerializeField] private float rotMin;
    [SerializeField] private float rotwave;

    public float speed;
    

   private void FixedUpdate()
    { 
       //Up Down
          
        transform.position = Vector2.Lerp(transform.position,new Vector2 (-0.25f,Mathf.PingPong(Time.time/7,waveMax)),Time.deltaTime);

        //Rotation

        rotwave = Mathf.Lerp(rotwave, Mathf.PingPong(Time.time/3,rotMax),Time.deltaTime);

       // rotwave = new Vector2 (0,Mathf.PingPong(Time.time/7,0.3f));        
       // float rot = Mathf.Lerp(rotMin,rotwave.y,Time.deltaTime);
       transform.rotation = Quaternion.Euler(0, 0, (transform.position.y*10)-2);
    }
}
