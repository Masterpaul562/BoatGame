using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{
   [SerializeField] private float waveMax;
   [SerializeField] private Vector2 wave;
   [SerializeField] private float rotMax;
    [SerializeField] private float rotwave;
    [SerializeField] private float offset;
    [SerializeField] private float offsetZ;

    public float speed;
    private void Start()
    {
        offset = transform.position.y;
        offsetZ = transform.rotation.z;
    }

   private void FixedUpdate()
    { 
       //Up Down
          
        wave = Vector2.Lerp(transform.position,new Vector2 (transform.position.x,offset-Mathf.PingPong(Time.time/speed,waveMax)),Time.deltaTime);
        transform.position = wave;
        //Rotation

        rotwave = Mathf.Lerp(rotwave, Mathf.PingPong(Time.time/3,rotMax),Time.deltaTime);

       // rotwave = new Vector2 (0,Mathf.PingPong(Time.time/7,0.3f));        
       // float rot = Mathf.Lerp(rotMin,rotwave.y,Time.deltaTime);
       transform.rotation = Quaternion.Euler(0, 0, ((transform.position.y+offsetZ)*10));
    }
}
